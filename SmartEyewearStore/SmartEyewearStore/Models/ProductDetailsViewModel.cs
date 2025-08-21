using System.Collections.Generic;

namespace SmartEyewearStore.Models
{
    public class ProductDetailsViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Brand { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int SelectedVariantId { get; set; }

        public List<VariantViewModel> Variants { get; set; } = new();
        public List<string> Features { get; set; } = new();

        public class VariantViewModel
        {
            public int VariantId { get; set; }
            public int ProductId { get; set; }
            public int? ColorId { get; set; }
            public string ColorName { get; set; } = string.Empty;
            public string? SizeLabel { get; set; }
            public string? Sku { get; set; }
            public int InventoryQty { get; set; }
            public PriceViewModel? Price { get; set; }
            public DimensionsViewModel? Dimensions { get; set; }
            public List<ImageViewModel> Images { get; set; } = new();
        }

        public class PriceViewModel
        {
            public string Currency { get; set; } = string.Empty;
            public int BasePriceCents { get; set; }
            public int? SalePriceCents { get; set; }
            public bool IsOnSale { get; set; }
        }

        public class DimensionsViewModel
        {
            public decimal? LensWidthMm { get; set; }
            public decimal? LensHeightMm { get; set; }
            public decimal? BridgeWidthMm { get; set; }
            public decimal? TempleLengthMm { get; set; }
            public decimal? FrameWidthMm { get; set; }
            public decimal? WeightG { get; set; }
        }

        public class ImageViewModel
        {
            public string Url { get; set; } = string.Empty;
            public string? Role { get; set; }
            public int SortOrder { get; set; }
            public string? Alt { get; set; }
        }
    }
}