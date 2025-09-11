using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEyewearStore.Data;
using SmartEyewearStore.Models;
using SmartEyewearStore.Models.Catalog;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SmartEyewearStore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CheckoutController(ApplicationDbContext db) => _db = db;

        int? CurrentUserId => HttpContext.Session.GetInt32("UserId");
        string CurrentGuestId => HttpContext.Session.GetString("GuestId") ?? "";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cart = await LoadActiveCartAsync();
            if (cart == null || !cart.Items.Any()) return RedirectToAction("Index", "Cart");

            var vm = await BuildCheckoutViewModelAsync(cart);
            PrefillForLoggedUser(vm);
            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Index(CheckoutViewModel vm)
        {
            var cart = await LoadActiveCartAsync();
            if (cart == null || !cart.Items.Any()) return RedirectToAction("Index", "Cart");
            var calc = await RecalculateAsync(cart);
            vm.SubtotalCents = calc.Subtotal;
            vm.ShippingCents = 0;
            vm.TaxCents = 0;
            vm.TotalCents = calc.Subtotal;

            if (!ModelState.IsValid)
            {
                vm.Lines = calc.Lines;
                return View(vm);
            }

            using var tx = await _db.Database.BeginTransactionAsync();

            foreach (var ci in cart.Items)
            {
                var inv = await _db.VariantInventories.FindAsync(ci.VariantId);
                if (inv == null || inv.QtyOnHand < ci.Qty)
                {
                    ModelState.AddModelError("", "موجودی کافی نیست.");
                    vm.Lines = calc.Lines;
                    return View(vm);
                }
            }

            var order = new Order
            {
                OrderNumber = await GenerateOrderNumberAsync(),
                UserId = CurrentUserId,
                GuestId = CurrentUserId.HasValue ? null : CurrentGuestId,
                CartId = cart.CartId,
                Status = "Pending",
                FullName = vm.FullName,
                Email = vm.Email,
                Phone = vm.Phone,
                BillingAddress1 = vm.BillingAddress1,
                BillingAddress2 = vm.BillingAddress2,
                BillingCity = vm.BillingCity,
                BillingState = vm.BillingState,
                BillingPostalCode = vm.BillingPostalCode,
                BillingCountry = vm.BillingCountry,
                ShipToDifferent = vm.ShipToDifferent,
                ShippingFullName = vm.ShippingFullName,
                ShippingPhone = vm.ShippingPhone,
                ShippingAddress1 = vm.ShippingAddress1,
                ShippingAddress2 = vm.ShippingAddress2,
                ShippingCity = vm.ShippingCity,
                ShippingState = vm.ShippingState,
                ShippingPostalCode = vm.ShippingPostalCode,
                ShippingCountry = vm.ShippingCountry,
                SubtotalCents = vm.SubtotalCents,
                ShippingCents = vm.ShippingCents,
                TaxCents = vm.TaxCents,
                TotalCents = vm.TotalCents
            };
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            foreach (var line in calc.Lines)
            {
                _db.OrderItems.Add(new OrderItem
                {
                    OrderId = order.OrderId,
                    VariantId = line.VariantId,
                    ProductName = line.ProductName,
                    VariantLabel = null,
                    UnitPriceCents = line.UnitPriceCents,
                    Qty = line.Qty
                });
                var inv = await _db.VariantInventories.FindAsync(line.VariantId);
                if (inv != null) inv.QtyOnHand -= line.Qty;
            }

            cart.ClosedAt = DateTime.UtcNow;
            var items = _db.CartItems.Where(i => i.CartId == cart.CartId);
            _db.CartItems.RemoveRange(items);

            await _db.SaveChangesAsync();
            await tx.CommitAsync();

            return RedirectToAction(nameof(ThankYou), new { orderNumber = order.OrderNumber });
        }

        [HttpGet]
        public async Task<IActionResult> ThankYou(string orderNumber)
        {
            if (string.IsNullOrWhiteSpace(orderNumber)) return RedirectToAction("Index", "Home");

            var order = await _db.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

            if (order == null) return NotFound();

            if (CurrentUserId.HasValue)
            {
                if (order.UserId != CurrentUserId.Value) return NotFound();
            }
            else
            {
                if (!string.Equals(order.GuestId, CurrentGuestId, StringComparison.Ordinal)) return NotFound();
            }

            return View(order);
        }

        async Task<Cart?> LoadActiveCartAsync()
        {
            if (CurrentUserId.HasValue)
            {
                return await _db.Carts
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.UserId == CurrentUserId.Value && c.ClosedAt == null);
            }
            var gid = CurrentGuestId;
            if (string.IsNullOrEmpty(gid)) return null;
            return await _db.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.GuestId == gid && c.ClosedAt == null);
        }

        async Task<CheckoutViewModel> BuildCheckoutViewModelAsync(Cart cart)
        {
            var now = DateTime.UtcNow;
            var variantIds = cart.Items.Select(i => i.VariantId).ToList();
            var variants = await _db.ProductVariants
                .Include(v => v.Product)
                .Include(v => v.Images)
                .Where(v => variantIds.Contains(v.VariantId))
                .ToListAsync();

            var lines = new List<CheckoutViewModel.Line>();
            int subtotal = 0;

            foreach (var ci in cart.Items)
            {
                var v = variants.FirstOrDefault(x => x.VariantId == ci.VariantId);
                if (v == null) continue;

                var price = await _db.VariantPrices
                    .Where(p => p.VariantId == v.VariantId && p.ValidFrom <= now && (p.ValidTo == null || p.ValidTo >= now))
                    .OrderByDescending(p => p.ValidFrom)
                    .FirstOrDefaultAsync();
                if (price == null)
                    price = await _db.VariantPrices.Where(p => p.VariantId == v.VariantId).OrderByDescending(p => p.ValidFrom).FirstOrDefaultAsync();

                int unit = price?.SalePriceCents ?? price?.BasePriceCents ?? 0;
                var img = v.Images.OrderBy(x => x.SortOrder).FirstOrDefault()?.Url ?? "/assets/images/placeholder.png";
                var lineTotal = unit * ci.Qty;
                subtotal += lineTotal;

                lines.Add(new CheckoutViewModel.Line
                {
                    CartItemId = ci.CartItemId,
                    VariantId = v.VariantId,
                    ProductName = v.Product?.Name ?? ("#" + v.VariantId),
                    ImageUrl = img,
                    UnitPriceCents = unit,
                    Qty = ci.Qty,
                    LineTotalCents = lineTotal
                });
            }

            return new CheckoutViewModel
            {
                SubtotalCents = subtotal,
                ShippingCents = 0,
                TaxCents = 0,
                TotalCents = subtotal,
                Lines = lines
            };
        }

        void PrefillForLoggedUser(CheckoutViewModel vm)
        {
            if (!CurrentUserId.HasValue) return;
            var user = _db.Users.FirstOrDefault(u => u.Id == CurrentUserId.Value);
            if (user == null) return;
            if (string.IsNullOrEmpty(vm.Email)) vm.Email = user.Email ?? "";
            if (string.IsNullOrEmpty(vm.FullName)) vm.FullName = user.Username ?? "";
        }

        async Task<(List<CheckoutViewModel.Line> Lines, int Subtotal)> RecalculateAsync(Cart cart)
        {
            var m = await BuildCheckoutViewModelAsync(cart);
            return (m.Lines, m.SubtotalCents);
        }

        async Task<string> GenerateOrderNumberAsync()
        {
            var rnd = new Random();
            while (true)
            {
                var token = "SO-" + Base36(rnd.NextInt64(1_000_000_000_000));
                if (!await _db.Orders.AnyAsync(o => o.OrderNumber == token)) return token;
            }
        }

        static string Base36(long value)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var b = new char[10];
            var i = 0;
            while (value > 0 && i < b.Length)
            {
                b[i++] = chars[(int)(value % 36)];
                value /= 36;
            }
            Array.Resize(ref b, i);
            Array.Reverse(b);
            return new string(b);
        }
    }
}
