namespace SmartEyewearStore.Models.Catalog
{
    public class VariantImage
    {
        public int ImageId { get; set; }
        public int VariantId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string? Role { get; set; }
        public int SortOrder { get; set; }

        public ProductVariant Variant { get; set; } = null!;
    }
}