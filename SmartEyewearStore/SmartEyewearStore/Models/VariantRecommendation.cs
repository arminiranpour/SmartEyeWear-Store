using SmartEyewearStore.Models.Catalog;

namespace SmartEyewearStore.Models
{
    public class VariantRecommendation
    {
        public ProductVariant Variant { get; set; } = null!;
        public double Score { get; set; }
    }
}
