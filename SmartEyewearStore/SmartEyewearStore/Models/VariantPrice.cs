using System;

namespace SmartEyewearStore.Models.Catalog
{
    public class VariantPrice
    {
        public int PriceId { get; set; }
        public int VariantId { get; set; }
        public string Currency { get; set; } = "USD";
        public int BasePriceCents { get; set; }
        public int? SalePriceCents { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public ProductVariant Variant { get; set; } = null!;
    }
}