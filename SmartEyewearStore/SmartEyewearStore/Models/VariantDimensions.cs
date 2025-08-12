namespace SmartEyewearStore.Models.Catalog
{
    public class VariantDimensions
    {
        public int VariantId { get; set; }
        public decimal LensWidthMm { get; set; }
        public decimal BridgeWidthMm { get; set; }
        public decimal TempleLengthMm { get; set; }
        public decimal? LensHeightMm { get; set; }
        public decimal? FrameWidthMm { get; set; }

        public ProductVariant Variant { get; set; } = null!;
    }
}