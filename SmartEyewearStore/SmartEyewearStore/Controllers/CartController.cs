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
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db) => _db = db;

        int? CurrentUserId => HttpContext.Session.GetInt32("UserId");

        string EnsureGuestId()
        {
            var gid = HttpContext.Session.GetString("GuestId");
            if (string.IsNullOrEmpty(gid))
            {
                gid = Guid.NewGuid().ToString("N");
                HttpContext.Session.SetString("GuestId", gid);
            }
            return gid;
        }

        async Task<Cart> GetOrCreateCartAsync()
        {
            Cart? cart;
            if (CurrentUserId.HasValue)
            {
                cart = await _db.Carts
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.UserId == CurrentUserId.Value);
                if (cart == null)
                {
                    cart = new Cart { UserId = CurrentUserId.Value };
                    _db.Carts.Add(cart);
                    await _db.SaveChangesAsync();
                }
            }
            else
            {
                var guestId = EnsureGuestId();
                cart = await _db.Carts
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.GuestId == guestId);
                if (cart == null)
                {
                    cart = new Cart { GuestId = guestId };
                    _db.Carts.Add(cart);
                    await _db.SaveChangesAsync();
                }
            }
            return cart;
        }

        // GET /cart
        public async Task<IActionResult> Index()
        {
            var vm = await BuildCartViewModelAsync();
            return View(vm);
        }

        // POST /cart/add
        [HttpPost]
        public async Task<IActionResult> Add(int variantId, int qty = 1)
        {
            if (qty < 1) qty = 1;

            // موجودی
            var inv = await _db.VariantInventories.FindAsync(variantId);
            if (inv == null || inv.QtyOnHand <= 0)
            {
                TempData["CartError"] = "این محصول ناموجود است.";
                return RedirectToAction(nameof(Index));
            }

            var cart = await GetOrCreateCartAsync();
            var item = cart.Items.FirstOrDefault(i => i.VariantId == variantId);
            var newQty = (item?.Qty ?? 0) + qty;

            if (newQty > inv.QtyOnHand)
            {
                TempData["CartError"] = $"حداکثر موجودی این محصول {inv.QtyOnHand} عدد است.";
                return RedirectToAction(nameof(Index));
            }

            if (item == null)
                _db.CartItems.Add(new CartItem { CartId = cart.CartId, VariantId = variantId, Qty = qty });
            else
                item.Qty = newQty;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST /cart/update
        [HttpPost]
        public async Task<IActionResult> UpdateQty(int itemId, int qty)
        {
            var cart = await GetOrCreateCartAsync();
            var item = await _db.CartItems.FirstOrDefaultAsync(i => i.CartItemId == itemId && i.CartId == cart.CartId);
            if (item == null) return RedirectToAction(nameof(Index));

            if (qty <= 0)
            {
                _db.CartItems.Remove(item);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var inv = await _db.VariantInventories.FindAsync(item.VariantId);
            if (inv == null || inv.QtyOnHand < qty)
            {
                TempData["CartError"] = inv == null ? "این محصول ناموجود است." : $"حداکثر موجودی {inv.QtyOnHand} عدد است.";
                return RedirectToAction(nameof(Index));
            }

            item.Qty = qty;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST /cart/remove
        [HttpPost]
        public async Task<IActionResult> Remove(int itemId)
        {
            var cart = await GetOrCreateCartAsync();
            var item = await _db.CartItems.FirstOrDefaultAsync(i => i.CartItemId == itemId && i.CartId == cart.CartId);
            if (item != null)
            {
                _db.CartItems.Remove(item);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST /cart/empty
        [HttpPost]
        public async Task<IActionResult> Empty()
        {
            var cart = await GetOrCreateCartAsync();
            var items = _db.CartItems.Where(i => i.CartId == cart.CartId);
            _db.CartItems.RemoveRange(items);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ---- helpers ----
        async Task<CartPageViewModel> BuildCartViewModelAsync()
        {
            var cart = await GetOrCreateCartAsync();

            var itemIds = cart.Items.Select(i => i.VariantId).ToList();
            var variants = await _db.ProductVariants
                .Include(v => v.Product)
                .Include(v => v.Images)
                .Where(v => itemIds.Contains(v.VariantId))
                .ToListAsync();

            var now = DateTime.UtcNow;
            int Subtotal = 0;
            var lines = new List<CartPageViewModel.Line>();
            string currency = "USD";

            foreach (var ci in cart.Items)
            {
                var v = variants.FirstOrDefault(x => x.VariantId == ci.VariantId);
                if (v == null) continue;

                var price = await _db.VariantPrices
                    .Where(p => p.VariantId == v.VariantId &&
                                p.ValidFrom <= now &&
                                (p.ValidTo == null || p.ValidTo >= now))
                    .OrderByDescending(p => p.ValidFrom)
                    .FirstOrDefaultAsync();

                if (price == null) // fallback: آخرین قیمت
                {
                    price = await _db.VariantPrices
                        .Where(p => p.VariantId == v.VariantId)
                        .OrderByDescending(p => p.ValidFrom)
                        .FirstOrDefaultAsync();
                }

                int unit = 0;
                if (price != null)
                {
                    currency = price.Currency;
                    unit = price.SalePriceCents ?? price.BasePriceCents;
                }

                var img = v.Images.OrderBy(x => x.SortOrder).FirstOrDefault()?.Url ?? "/assets/images/placeholder.png";
                lines.Add(new CartPageViewModel.Line
                {
                    CartItemId = ci.CartItemId,
                    VariantId = v.VariantId,
                    ProductName = v.Product?.Name ?? $"Variant #{v.VariantId}",
                    ImageUrl = img,
                    UnitPriceCents = unit,
                    Qty = ci.Qty
                });
                Subtotal += unit * ci.Qty;
            }

            return new CartPageViewModel
            {
                Items = lines,
                SubtotalCents = Subtotal,
                Currency = currency,
                ErrorMessage = TempData["CartError"] as string
            };
        }
    }
}
