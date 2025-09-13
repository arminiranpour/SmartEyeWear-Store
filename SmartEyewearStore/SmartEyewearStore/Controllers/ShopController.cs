using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEyewearStore.Data;
using SmartEyewearStore.ViewModels;

namespace SmartEyewearStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const int DefaultPageSize = 24;
        private const int MaxPageSize = 60;

        public ShopController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] CatalogFiltersVM filters)
        {
            filters.Page = Math.Max(filters.Page, 1);
            if (filters.PageSize <= 0) filters.PageSize = DefaultPageSize;
            filters.PageSize = Math.Clamp(filters.PageSize, 1, MaxPageSize);

            // CHANGE: چون فیلتر قیمت حذف شد، نیازی به جابجایی Min/Max نیست
            // اگر در آینده برگردوندی، این بخش را احیا کن
            // if (filters.PriceMin.HasValue && filters.PriceMax.HasValue && filters.PriceMin > filters.PriceMax) { ... }

            filters.Search = filters.Search?.Trim();
            var now = DateTime.UtcNow;

            // Filter option lists
            var brandList = await _db.Brands.OrderBy(b => b.Name).Select(b => new { b.BrandId, b.Name }).ToListAsync();
            filters.Brands = brandList.Select(b => new SelectOption { Id = b.BrandId, Label = b.Name, Selected = filters.BrandIds?.Contains(b.BrandId) == true }).ToList();

            var shapeList = await _db.Shapes.OrderBy(s => s.Name).Select(s => new { s.ShapeId, s.Name }).ToListAsync();
            filters.Shapes = shapeList.Select(s => new SelectOption { Id = s.ShapeId, Label = s.Name, Selected = filters.ShapeIds?.Contains(s.ShapeId) == true }).ToList();

            var colorList = await _db.Colors.OrderBy(c => c.Name).Select(c => new { c.ColorId, c.Name }).ToListAsync();
            filters.Colors = colorList.Select(c => new SelectOption { Id = c.ColorId, Label = c.Name, Selected = filters.ColorIds?.Contains(c.ColorId) == true }).ToList();

            var materialList = await _db.Materials.OrderBy(m => m.Name).Select(m => new { m.MaterialId, m.Name }).ToListAsync();
            filters.Materials = materialList.Select(m => new SelectOption { Id = m.MaterialId, Label = m.Name, Selected = filters.MaterialIds?.Contains(m.MaterialId) == true }).ToList();

            var rimStyleList = await _db.RimStyles.OrderBy(r => r.Name).Select(r => new { r.RimStyleId, r.Name }).ToListAsync();
            filters.RimStyles = rimStyleList.Select(r => new SelectOption { Id = r.RimStyleId, Label = r.Name, Selected = filters.RimStyleIds?.Contains(r.RimStyleId) == true }).ToList();

            var featureList = await _db.Features.OrderBy(f => f.Label).Select(f => new { f.FeatureId, f.Label }).ToListAsync();
            filters.Features = featureList.Select(f => new SelectOption { Id = f.FeatureId, Label = f.Label, Selected = filters.FeatureIds?.Contains(f.FeatureId) == true }).ToList();

            // CHANGE: سایزها از UI حذف شد؛ این بخش را هم غیرفعال می‌کنیم
            // (اگر بعداً نیاز شد، برگردان)
            // var sizeLabels = await _db.ProductVariants
            //     .Select(v => v.SizeLabel).Where(s => s != null && s != "")
            //     .Distinct().OrderBy(s => s).ToListAsync();
            // filters.SizesOptions = sizeLabels
            //     .Select(s => new SelectOption { Id = 0, Label = s!, Selected = filters.Sizes != null && filters.Sizes.Contains(s!) })
            //     .ToList();

            // CHANGE: محدوده قیمت قابل‌دسترسی برای UI لازم نیست
            // var activePricesList = await _db.VariantPrices ...;
            // filters.PriceMinAvailable = ...;
            // filters.PriceMaxAvailable = ...;

            // Base product-variant query
            var query = _db.ProductVariants
                .Include(v => v.Product).ThenInclude(p => p.Brand)
                .Include(v => v.Product).ThenInclude(p => p.FrameSpecs)
                .Include(v => v.Color)
                .Include(v => v.Images)
                .Include(v => v.Prices)
                .AsQueryable();

            // Apply filters
            if (filters.BrandIds?.Any() == true)
                query = query.Where(v => v.Product.BrandId != null && filters.BrandIds.Contains(v.Product.BrandId.Value));

            if (filters.ShapeIds?.Any() == true)
                query = query.Where(v => v.Product.FrameSpecs != null
                    && v.Product.FrameSpecs.ShapeId != null
                    && filters.ShapeIds.Contains(v.Product.FrameSpecs.ShapeId.Value));

            if (filters.ColorIds?.Any() == true)
                query = query.Where(v => v.ColorId != null && filters.ColorIds.Contains(v.ColorId.Value));

            if (filters.MaterialIds?.Any() == true)
                query = query.Where(v => v.Product.FrameSpecs != null
                    && v.Product.FrameSpecs.MaterialId != null
                    && filters.MaterialIds.Contains(v.Product.FrameSpecs.MaterialId.Value));

            if (filters.RimStyleIds?.Any() == true)
                query = query.Where(v => v.Product.FrameSpecs != null
                    && v.Product.FrameSpecs.RimStyleId != null
                    && filters.RimStyleIds.Contains(v.Product.FrameSpecs.RimStyleId.Value));

            // CHANGE: فیلتر سایز حذف شد
            // if (filters.Sizes?.Any() == true)
            //     query = query.Where(v => v.SizeLabel != null && filters.Sizes.Contains(v.SizeLabel));

            if (filters.FeatureIds?.Any() == true)
                query = query.Where(v => v.Product.ProductFeatures.Any(pf => filters.FeatureIds.Contains(pf.FeatureId)));

            if (!string.IsNullOrEmpty(filters.Search))
            {
                var searchLower = filters.Search.ToLower();
                query = query.Where(v =>
                    v.Product.Name.ToLower().Contains(searchLower) ||
                    v.Product.Slug.ToLower().Contains(searchLower));
            }

            var queryWithCalc = query
                .Select(v => new
                {
                    Variant = v,
                    ActivePrice = v.Prices
                        .Where(p => p.ValidFrom <= now && (p.ValidTo == null || p.ValidTo >= now))
                        .OrderBy(p => p.SalePriceCents ?? p.BasePriceCents)
                        .FirstOrDefault(),
                    PrimaryImage = v.Images.OrderBy(i => i.SortOrder).FirstOrDefault(),
                    PriceValue = (
                        v.Prices
                         .Where(p => p.ValidFrom <= now && (p.ValidTo == null || p.ValidTo >= now))
                         .OrderBy(p => p.SalePriceCents ?? p.BasePriceCents)
                         .Select(p => (decimal?)((p.SalePriceCents ?? p.BasePriceCents) / 100m))
                         .FirstOrDefault()
                    )
                });

            // CHANGE: فیلتر محدوده قیمت را حذف می‌کنیم (چون UI ندارد)
            // if (filters.PriceMin.HasValue) { ... }
            // if (filters.PriceMax.HasValue) { ... }

            // CHANGE: گروه‌بندی با MinPrice امن (اگر null بود => 0)
            var grouped = queryWithCalc
                .GroupBy(x => new
                {
                    x.Variant.Product.ProductId,
                    x.Variant.Product.Slug,
                    x.Variant.Product.Name,
                    BrandName = x.Variant.Product.Brand != null ? x.Variant.Product.Brand.Name : null,
                    x.Variant.Product.CreatedAt
                })
                .Select(g => new
                {
                    g.Key.ProductId,
                    g.Key.Slug,
                    g.Key.Name,
                    g.Key.BrandName,
                    g.Key.CreatedAt,
                    MinPrice = (g.Min(x => x.PriceValue) ?? 0m),
                    PrimaryImage =
    g.OrderBy(x => x.Variant.VariantId)                     
     .SelectMany(x => x.Variant.Images)
     .Where(i => !string.IsNullOrWhiteSpace(i.Url))
     .OrderBy(i => i.SortOrder)                              
     .Select(i => i.Url.Trim())
     .FirstOrDefault()
    ?? g.SelectMany(x => x.Variant.Images)
        .Where(i => !string.IsNullOrWhiteSpace(i.Url))
        .OrderBy(i => i.SortOrder)
        .Select(i => i.Url.Trim())
        .FirstOrDefault(),

                    Colors = g.Where(x => x.Variant.Color != null)
                        .Select(x => x.Variant.Color!.Name!)
                        .Distinct(),
                    Sizes = g.Select(x => x.Variant.SizeLabel).Where(s => s != null).Distinct()
                });

            var totalItems = await grouped.CountAsync();

            // Sorting
            switch (filters.Sort)
            {
                case "price_asc":
                    grouped = grouped.OrderBy(g => g.MinPrice);
                    break;
                case "price_desc":
                    grouped = grouped.OrderByDescending(g => g.MinPrice);
                    break;
                case "newest":
                    grouped = grouped.OrderByDescending(g => g.CreatedAt);
                    break;
                default:
                    grouped = grouped.OrderBy(g => g.Name);
                    break;
            }

            // Pagination & projection
            var items = await grouped
                .Skip((filters.Page - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .Select(g => new ProductCardVM
                {
                    ProductId = g.ProductId,
                    Slug = g.Slug,
                    Name = g.Name,
                    BrandName = g.BrandName,
                    MinPrice = g.MinPrice,          
                    PrimaryImageUrl = g.PrimaryImage,
                    Colors = g.Colors,
                    SizeLabel = g.Sizes.FirstOrDefault()
                })
                .ToListAsync();

            var vm = new CatalogPageVM
            {
                Items = items,
                Filters = filters,
                TotalItems = totalItems,
                Page = filters.Page,
                PageSize = filters.PageSize,
                TotalPages = (int)Math.Ceiling(totalItems / (double)filters.PageSize)
            };

            return View("~/Views/Store/Shop.cshtml", vm);
        }
    }
}
