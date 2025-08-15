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
            if (filters.PriceMin.HasValue && filters.PriceMax.HasValue && filters.PriceMin > filters.PriceMax)
            {
                var tmp = filters.PriceMin; filters.PriceMin = filters.PriceMax; filters.PriceMax = tmp;
            }
            filters.Search = filters.Search?.Trim();

            var now = DateTime.UtcNow;

            filters.Brands = await _db.Brands
                .OrderBy(b => b.Name)
                .Select(b => new SelectOption { Id = b.BrandId, Label = b.Name, Selected = filters.BrandIds != null && filters.BrandIds.Contains(b.BrandId) })
                .ToListAsync();
            filters.Shapes = await _db.Shapes
                .OrderBy(s => s.Name)
                .Select(s => new SelectOption { Id = s.ShapeId, Label = s.Name, Selected = filters.ShapeIds != null && filters.ShapeIds.Contains(s.ShapeId) })
                .ToListAsync();
            filters.Colors = await _db.Colors
                .OrderBy(c => c.Name)
                .Select(c => new SelectOption { Id = c.ColorId, Label = c.Name, Selected = filters.ColorIds != null && filters.ColorIds.Contains(c.ColorId) })
                .ToListAsync();
            filters.Materials = await _db.Materials
                .OrderBy(m => m.Name)
                .Select(m => new SelectOption { Id = m.MaterialId, Label = m.Name, Selected = filters.MaterialIds != null && filters.MaterialIds.Contains(m.MaterialId) })
                .ToListAsync();
            filters.RimStyles = await _db.RimStyles
                .OrderBy(r => r.Name)
                .Select(r => new SelectOption { Id = r.RimStyleId, Label = r.Name, Selected = filters.RimStyleIds != null && filters.RimStyleIds.Contains(r.RimStyleId) })
                .ToListAsync();
            filters.Features = await _db.Features
                .OrderBy(f => f.Label)
                .Select(f => new SelectOption { Id = f.FeatureId, Label = f.Label, Selected = filters.FeatureIds != null && filters.FeatureIds.Contains(f.FeatureId) })
                .ToListAsync();

            var sizeLabels = await _db.ProductVariants
                .Select(v => v.SizeLabel)
                .Where(s => s != null && s != "")
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();
            filters.SizesOptions = sizeLabels.Select(s => new SelectOption { Id = 0, Label = s!, Selected = filters.Sizes != null && filters.Sizes.Contains(s!) }).ToList();

            var activePrices = _db.VariantPrices
                .Where(p => p.ValidFrom <= now && (p.ValidTo == null || p.ValidTo >= now))
                .Select(p => (decimal)(p.SalePriceCents ?? p.BasePriceCents) / 100m);
            filters.PriceMinAvailable = activePrices.Any() ? await activePrices.MinAsync() : 0;
            filters.PriceMaxAvailable = activePrices.Any() ? await activePrices.MaxAsync() : 0;

            var query = _db.ProductVariants
                .Include(v => v.Product).ThenInclude(p => p.Brand)
                .Include(v => v.Product).ThenInclude(p => p.FrameSpecs)
                .Include(v => v.Color)
                .Include(v => v.Images)
                .Include(v => v.Prices)
                .Where(v => v.Product.IsActive == true)
                .AsQueryable();

            if (filters.BrandIds?.Any() == true)
                query = query.Where(v => v.Product.BrandId != null && filters.BrandIds.Contains(v.Product.BrandId.Value));
            if (filters.ShapeIds?.Any() == true)
                query = query.Where(v => v.Product.FrameSpecs != null && v.Product.FrameSpecs.ShapeId != null && filters.ShapeIds.Contains(v.Product.FrameSpecs.ShapeId.Value));
            if (filters.ColorIds?.Any() == true)
                query = query.Where(v => v.ColorId != null && filters.ColorIds.Contains(v.ColorId.Value));
            if (filters.MaterialIds?.Any() == true)
                query = query.Where(v => v.Product.FrameSpecs != null && v.Product.FrameSpecs.MaterialId != null && filters.MaterialIds.Contains(v.Product.FrameSpecs.MaterialId.Value));
            if (filters.RimStyleIds?.Any() == true)
                query = query.Where(v => v.Product.FrameSpecs != null && v.Product.FrameSpecs.RimStyleId != null && filters.RimStyleIds.Contains(v.Product.FrameSpecs.RimStyleId.Value));
            if (filters.Sizes?.Any() == true)
                query = query.Where(v => v.SizeLabel != null && filters.Sizes.Contains(v.SizeLabel));
            if (filters.FeatureIds?.Any() == true)
                query = query.Where(v => v.Product.ProductFeatures.Any(pf => filters.FeatureIds.Contains(pf.FeatureId)));
            if (!string.IsNullOrEmpty(filters.Search))
            {
                var searchLower = filters.Search.ToLower();
                query = query.Where(v => v.Product.Name.ToLower().Contains(searchLower) || v.Product.Slug.ToLower().Contains(searchLower));
            }

            var queryWithPrice = query
                .Select(v => new
                {
                    Variant = v,
                    ActivePrice = v.Prices.Where(p => p.ValidFrom <= now && (p.ValidTo == null || p.ValidTo >= now))
                        .OrderBy(p => p.SalePriceCents ?? p.BasePriceCents)
                        .FirstOrDefault(),
                    PrimaryImage = v.Images.OrderBy(i => i.SortOrder).FirstOrDefault()
                })
                .Where(x => x.ActivePrice != null);

            if (filters.PriceMin.HasValue)
            {
                var min = filters.PriceMin.Value;
                queryWithPrice = queryWithPrice.Where(x => (decimal)(x.ActivePrice.SalePriceCents ?? x.ActivePrice.BasePriceCents) / 100m >= min);
            }
            if (filters.PriceMax.HasValue)
            {
                var max = filters.PriceMax.Value;
                queryWithPrice = queryWithPrice.Where(x => (decimal)(x.ActivePrice.SalePriceCents ?? x.ActivePrice.BasePriceCents) / 100m <= max);
            }

            var grouped = queryWithPrice
                .GroupBy(x => x.Variant.Product)
                .Select(g => new
                {
                    Product = g.Key,
                    MinPrice = g.Min(x => (decimal)(x.ActivePrice.SalePriceCents ?? x.ActivePrice.BasePriceCents) / 100m),
                    PrimaryImage = g.OrderBy(x => (decimal)(x.ActivePrice.SalePriceCents ?? x.ActivePrice.BasePriceCents) / 100m)
                        .Select(x => x.PrimaryImage != null ? x.PrimaryImage.Url : null)
                        .FirstOrDefault(),
                    Colors = g.Select(x => x.Variant.Color != null ? x.Variant.Color.Name : null).Distinct(),
                    Sizes = g.Select(x => x.Variant.SizeLabel).Where(s => s != null).Distinct()
                });

            var totalItems = await grouped.CountAsync();

            switch (filters.Sort)
            {
                case "price_asc":
                    grouped = grouped.OrderBy(g => g.MinPrice);
                    break;
                case "price_desc":
                    grouped = grouped.OrderByDescending(g => g.MinPrice);
                    break;
                case "newest":
                    grouped = grouped.OrderByDescending(g => g.Product.CreatedAt);
                    break;
                default:
                    grouped = grouped.OrderBy(g => g.Product.Name);
                    break;
            }

            var items = await grouped
                .Skip((filters.Page - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .Select(g => new ProductCardVM
                {
                    ProductId = g.Product.ProductId,
                    Slug = g.Product.Slug,
                    Name = g.Product.Name,
                    BrandName = g.Product.Brand != null ? g.Product.Brand.Name : null,
                    MinPrice = g.MinPrice,
                    PrimaryImageUrl = g.PrimaryImage,
                    Colors = g.Colors.Where(c => c != null)!,
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

            return View("Shop", vm);
        }
    }
}