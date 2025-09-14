using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEyewearStore.Data;
using SmartEyewearStore.Models;
using SmartEyewearStore.ViewModels;
using System.Diagnostics;

namespace SmartEyewearStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            ViewData["UserId"] = HttpContext.Session.GetInt32("UserId");

            var picks = new (string slug, int variantId)[] {
                ("samba", 251),
                ("limoncello", 252),
                ("redwood", 257),
                ("santa-cruz", 262)
            };

            var now = DateTime.UtcNow;
            var popular = new List<ProductCardVM>();

            foreach (var p in picks)
            {
                var product = _db.Products
                    .Include(x => x.Brand)
                    .Include(x => x.Variants).ThenInclude(v => v.Images)
                    .Include(x => x.Variants).ThenInclude(v => v.Prices)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Slug == p.slug);

                if (product == null) continue;

                var variant = product.Variants.FirstOrDefault(v => v.VariantId == p.variantId);
                if (variant == null) continue;

                var mainImage = variant.Images
                    .OrderBy(i => i.SortOrder)
                    .LastOrDefault();

                var activePrice = variant.Prices
                    .Where(pr => pr.ValidFrom <= now && (pr.ValidTo == null || pr.ValidTo >= now))
                    .OrderBy(pr => pr.ValidFrom)
                    .FirstOrDefault()
                    ?? variant.Prices.OrderByDescending(pr => pr.ValidFrom).FirstOrDefault();

                var finalPrice = activePrice != null
                    ? (activePrice.SalePriceCents.HasValue ? activePrice.SalePriceCents.Value : activePrice.BasePriceCents) / 100m
                    : 0m;

                popular.Add(new ProductCardVM
                {
                    ProductId = product.ProductId,
                    Slug = product.Slug,
                    Name = product.Name,
                    BrandName = product.Brand?.Name,
                    MinPrice = finalPrice,
                    PrimaryImageUrl = mainImage?.Url,
                    VariantId = variant.VariantId
                });
            }

            ViewBag.PopularProducts = popular;
            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
