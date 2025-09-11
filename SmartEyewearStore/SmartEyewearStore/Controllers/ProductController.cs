using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEyewearStore.Data;
using SmartEyewearStore.Models;
using SmartEyewearStore.Models.Catalog;
using System;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace SmartEyewearStore.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{slug}/{variantId:int?}")]
        public async Task<IActionResult> Details(string slug, int? variantId)
        {
            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.RatingSummary)
                .Include(p => p.FrameSpecs).ThenInclude(fs => fs.Material)
                .Include(p => p.FrameSpecs).ThenInclude(fs => fs.Shape)
                .Include(p => p.FrameSpecs).ThenInclude(fs => fs.RimStyle)
                .Include(p => p.ProductFeatures).ThenInclude(pf => pf.Feature)
                .Include(p => p.Variants).ThenInclude(v => v.Color)
                .Include(p => p.Variants).ThenInclude(v => v.Images)
                .Include(p => p.Variants).ThenInclude(v => v.Prices)
                .Include(p => p.Variants).ThenInclude(v => v.Inventory)
                .Include(p => p.Variants).ThenInclude(v => v.Dimensions)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Slug == slug);

            if (product == null)
                return NotFound();

            if (variantId == null)
            {
                var first = product.Variants.FirstOrDefault();
                if (first == null)
                    return NotFound();
                return RedirectToActionPermanent(nameof(Details), new { slug, variantId = first.VariantId });
            }

            var variant = product.Variants.FirstOrDefault(v => v.VariantId == variantId);
            if (variant == null)
                return NotFound();

            var viewModel = BuildViewModel(product, variant);

            ViewData["Canonical"] = Url.Action(
                nameof(Details),
                "Product",
                new { slug = product.Slug, variantId = variant.VariantId },
                Request?.Scheme
            );

            return View(viewModel);
        }

        [HttpGet("{slug}/color-{colorId:int}")]
        public async Task<IActionResult> ByColor(string slug, int colorId)
        {
            var product = await _context.Products
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(p => p.Slug == slug);

            if (product == null)
                return NotFound();

            var variant = product.Variants.FirstOrDefault(v => v.ColorId == colorId);
            if (variant == null)
                return NotFound();

            return RedirectToActionPermanent(nameof(Details), new { slug, variantId = variant.VariantId });
        }

        private ProductDetailsViewModel BuildViewModel(Product product, ProductVariant selectedVariant)
        {
            var vm = new ProductDetailsViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Brand = product.Brand?.Name,
                Slug = product.Slug,
                Description = product.Description,
                SelectedVariantId = selectedVariant.VariantId,
                Features = product.ProductFeatures.Select(pf => pf.Feature.Label).ToList(),
                AvgRating = product.RatingSummary?.AvgRating,
                RatingCount = product.RatingSummary?.RatingCount,
                MaterialName = product.FrameSpecs?.Material?.Name,
                ShapeName = product.FrameSpecs?.Shape?.Name,
                RimStyleName = product.FrameSpecs?.RimStyle?.Name
            };

            vm.Variants = product.Variants
                .OrderBy(v => v.Color != null ? v.Color.Name : string.Empty)
                .Select(v => new ProductDetailsViewModel.VariantViewModel
                {
                    VariantId = v.VariantId,
                    ProductId = v.ProductId,
                    ColorId = v.ColorId,
                    ColorName = v.Color?.Name ?? string.Empty,
                    SizeLabel = v.SizeLabel,
                    Sku = v.Sku,
                    InventoryQty = v.Inventory?.QtyOnHand ?? 0,
                    Price = MapPrice(v),
                    Dimensions = v.Dimensions != null ? new ProductDetailsViewModel.DimensionsViewModel
                    {
                        LensWidthMm = v.Dimensions.LensWidthMm,
                        LensHeightMm = v.Dimensions.LensHeightMm,
                        BridgeWidthMm = v.Dimensions.BridgeWidthMm,
                        TempleLengthMm = v.Dimensions.TempleLengthMm,
                        FrameWidthMm = v.Dimensions.FrameWidthMm,
                        WeightG = product.FrameSpecs?.WeightG
                    } : null,
                    Images = v.Images
                        .OrderBy(i => i.SortOrder)
                        .Select(i => new ProductDetailsViewModel.ImageViewModel
                        {
                            Url = i.Url,
                            Role = i.Role,
                            SortOrder = i.SortOrder,
                            Alt = $"{product.Name} {v.Color?.Name}"
                        }).ToList()
                }).ToList();

            return vm;
        }

        private ProductDetailsViewModel.PriceViewModel? MapPrice(ProductVariant variant)
        {
            var now = DateTime.UtcNow;
            var price = variant.Prices
                .Where(p => p.ValidFrom <= now && (p.ValidTo == null || p.ValidTo >= now))
                .OrderBy(p => p.ValidFrom)
                .FirstOrDefault()
                ?? variant.Prices.OrderByDescending(p => p.ValidFrom).FirstOrDefault();

            if (price == null)
                return null;

            return new ProductDetailsViewModel.PriceViewModel
            {
                Currency = price.Currency,
                BasePriceCents = price.BasePriceCents,
                SalePriceCents = price.SalePriceCents,
                IsOnSale = price.SalePriceCents.HasValue
            };
        }
    }
}
