namespace SmartEyewearStore.Models.Catalog
{
    public class ProductFeature
    {
        public int ProductId { get; set; }
        public int FeatureId { get; set; }

        public Product Product { get; set; } = null!;
        public Feature Feature { get; set; } = null!;
    }
}