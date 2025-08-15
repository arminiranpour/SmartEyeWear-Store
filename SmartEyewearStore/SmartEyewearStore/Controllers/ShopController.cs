using System;
using System.Collections.Generic;
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
        private readonly ApplicationDbContext _context;
        private const int DefaultPageSize = 24;
        private const int MaxPageSize = 60;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] CatalogFiltersVM filters)
        {
            filters ??= new CatalogFiltersVM();
            filters.Page = filters.Page <= 0 ? 1 : filters.Page;
            filters.PageSize = filters.PageSize <= 0 ? DefaultPageSize : Math.Min(filters.PageSize, MaxPageSize);
            filters.Search = filters.Search?.Trim();
            if (filters.PriceMin.HasValue && filters.PriceMax.HasValue && filters.PriceMin > filters.PriceMax)
            {
                var tmp = filters.PriceMin;
                filters.PriceMin = filters.PriceMax;
                filters.PriceMax = tmp;
            }

            var today = DateTime.UtcNow;

            // global price bounds
            var priceQuery = _context.VariantPrices
                .Where(p => p.ValidFrom <= today && (p.ValidTo == null || p.ValidTo >= today));
            if (await priceQuery.AnyAsync())
            {
                filters.PriceMinAvailable = await priceQuery.MinAsync(p => (decimal)(p.SalePriceCents ?? p.BasePriceCents) / 100m);
                filters.PriceMaxAvailable = await priceQuery.MaxAsync(p => (decimal)(p.SalePriceCents ?? p.BasePriceCents) / 100m);
            }

            // options
            filters.Brands = await _context.Brands.AsNoTracking()
                .OrderBy(b => b.Name)
                .Select(b => new SelectOption { Id = b.BrandId, Label = b.Name, Selected = filters.BrandIds != null && filters.BrandIds.Contains(b.BrandId) })
                .ToListAsync();
            filters.Shapes = await _context.Shapes.AsNoTracking()
                .OrderBy(s => s.Name)
                .Select(s => new SelectOption { Id = s.ShapeId, Label = s.Name, Selected = filters.ShapeIds != null && filters.ShapeIds.Contains(s.ShapeId) })
                .ToListAsync();
            filters.Colors = await _context.Colors.AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new SelectOption { Id = c.ColorId, Label = c.Name, Selected = filters.ColorIds != null && filters.ColorIds.Contains(c.ColorId) })
                .ToListAsync();
            filters.Materials = await _context.Materials.AsNoTracking()
                .OrderBy(m => m.Name)
                .Select(m => new SelectOption { Id = m.MaterialId, Label = m.Name, Selected = filters.MaterialIds != null && filters.MaterialIds.Contains(m.MaterialId) })
                .ToListAsync();
            filters.RimStyles = await _context.RimStyles.AsNoTracking()
                .OrderBy(r => r.Name)
                .Select(r => new SelectOption { Id = r.RimStyleId, Label = r.Name, Selected = filters.RimStyleIds != null && filters.RimStyleIds.Contains(r.RimStyleId) })
                .ToListAsync();
            filters.Features = await _context.Features.AsNoTracking()
                .OrderBy(f => f.Name)
                .Select(f => new SelectOption { Id = f.FeatureId, Label = f.Name, Selected = filters.FeatureIds != null && filters.FeatureIds.Contains(f.FeatureId) })
                .ToListAsync();

            var sizeLabels = await _context.ProductVariants.AsNoTracking()
                .Select(v => v.SizeLabel)
                .Where(s => s != null && s != "")
                .Distinct()
                .ToListAsync();
            filters.SizesOptions = sizeLabels.Select((s, idx) => new SelectOption { Id = idx, Label = s!, Selected = filters.Sizes != null && filters.Sizes.Contains(s!) }).ToList();

            var fitLabelMap = new Dictionary<int, string> { { 0, "Narrow" }, { 1, "Regular" }, { 2, "Wide" } };
            var fitValues = await _context.ProductVariants.AsNoTracking()
                .Select(v => v.Fit)
                .Where(f => f != null)
                .Distinct()
                .ToListAsync();
            filters.FitOptions = fitValues.Select(f => new SelectOption { Id = f!.Value, Label = fitLabelMap.ContainsKey(f.Value) ? fitLabelMap[f.Value] : f.Value.ToString(), Selected = filters.Fit != null && filters.Fit.Contains(f.Value.ToString()) }).ToList();

            // query
            var fitInts = filters.Fit?.Select(f => int.TryParse(f, out var x) ? (int?)x : null).Where(x => x.HasValue).Select(x => x!.Value).ToArray();
            var priceMinCents = filters.PriceMin.HasValue ? (int)(filters.PriceMin.Value * 100m) : (int?)null;
            var priceMaxCents = filters.PriceMax.HasValue ? (int)(filters.PriceMax.Value * 100m) : (int?)null;

            var query = _context.ProductVariants
                .AsNoTracking()
                .Include(v => v.Product).ThenInclude(p => p.Brand)
                .Include(v => v.Product).ThenInclude(p => p.FrameSpecs)
                .Include(v => v.Product).ThenInclude(p => p.ProductFeatures)
                .Include(v => v.Color)
                .Include(v => v.Images)
                .Include(v => v.Prices)
                .Where(v => v.Product.IsActive == true);

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
            if (filters.FeatureIds?.Any() == true)
                query = query.Where(v => v.Product.ProductFeatures.Any(pf => filters.FeatureIds.Contains(pf.FeatureId)));
            if (filters.Sizes?.Any() == true)
                query = query.Where(v => v.SizeLabel != null && filters.Sizes.Contains(v.SizeLabel));
            if (fitInts?.Any() == true)
                query = query.Where(v => v.Fit != null && fitInts.Contains(v.Fit.Value));
            if (priceMinCents.HasValue || priceMaxCents.HasValue)
            {
                query = query.Where(v => v.Prices.Any(p =>
                    p.ValidFrom <= today && (p.ValidTo == null || p.ValidTo >= today) &&
                    (!priceMinCents.HasValue || (p.SalePriceCents ?? p.BasePriceCents) >= priceMinCents.Value) &&
                    (!priceMaxCents.HasValue || (p.SalePriceCents ?? p.BasePriceCents) <= priceMaxCents.Value)));
            }
            if (!string.IsNullOrWhiteSpace(filters.Search))
            {
                var s = filters.Search.ToLower();
                query = query.Where(v => v.Product.Name.ToLower().Contains(s) || v.Product.Slug.ToLower().Contains(s));
            }

            var preGroup = query.Select(v => new
            {
                v.Product.ProductId,
                v.Product.Slug,
                v.Product.Name,
                v.Product.CreatedAt,
                BrandName = v.Product.Brand != null ? v.Product.Brand.Name : null,
                v.SizeLabel,
                v.Fit,
                ColorName = v.Color != null ? v.Color.Name : null,
                PriceCents = v.Prices.Where(p => p.ValidFrom <= today && (p.ValidTo == null || p.ValidTo >= today))
                                     .OrderBy(p => p.SalePriceCents ?? p.BasePriceCents)
                                     .Select(p => p.SalePriceCents ?? p.BasePriceCents)
                                     .FirstOrDefault(),
                ImageUrl = v.Images.OrderBy(i => i.SortOrder).Select(i => i.Url).FirstOrDefault()
            });

            var grouped = preGroup.GroupBy(x => new { x.ProductId, x.Slug, x.Name, x.BrandName, x.CreatedAt });

            switch (filters.Sort)
            {
                case "price_asc":
                    grouped = grouped.OrderBy(g => g.Min(x => x.PriceCents));
                    break;
                case "price_desc":
                    grouped = grouped.OrderByDescending(g => g.Min(x => x.PriceCents));
                    break;
                case "newest":
                    grouped = grouped.OrderByDescending(g => g.Key.CreatedAt);
                    break;
                default:
                    grouped = grouped.OrderBy(g => g.Key.Name);
                    break;
            }

            var productQuery = grouped.Select(g => new ProductCardVM
            {
                ProductId = g.Key.ProductId,
                Slug = g.Key.Slug,
                Name = g.Key.Name,
                BrandName = g.Key.BrandName,
                MinPrice = g.Min(x => (decimal)x.PriceCents / 100m),
                PrimaryImageUrl = g.OrderBy(x => x.PriceCents).Select(x => x.ImageUrl).FirstOrDefault(),
                Colors = g.Select(x => x.ColorName).Where(n => n != null).Distinct().Cast<string>(),
                SizeLabel = g.Select(x => x.SizeLabel).FirstOrDefault(),
                Fit = g.Select(x => x.Fit.HasValue ? x.Fit.Value.ToString() : null).FirstOrDefault()
            });

            var totalItems = await productQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)filters.PageSize);

            var items = await productQuery
                .Skip((filters.Page - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .ToListAsync();

            var vm = new CatalogPageVM
            {
                Items = items,
                Filters = filters,
                TotalItems = totalItems,
                Page = filters.Page,
                PageSize = filters.PageSize,
                TotalPages = totalPages
            };

            return View("Shop", vm);
        }
    }
}
