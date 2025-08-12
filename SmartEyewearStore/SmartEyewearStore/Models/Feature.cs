using System.Collections.Generic;

namespace SmartEyewearStore.Models.Catalog
{
    public class Feature
    {
        public int FeatureId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;

        public ICollection<ProductFeature> ProductFeatures { get; set; } = new HashSet<ProductFeature>();
    }
}