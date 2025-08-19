using System;
using System.Collections.Generic;

namespace SmartEyewearStore.Models.Catalog
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? BrandId { get; set; }
        public string? Description { get; set; }
        public string? SourceUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Brand? Brand { get; set; }
        public FrameSpecs? FrameSpecs { get; set; }
        public ICollection<ProductVariant> Variants { get; set; } = new HashSet<ProductVariant>();
        public ICollection<ProductFeature> ProductFeatures { get; set; } = new HashSet<ProductFeature>();
        public ICollection<ProductTag> ProductTags { get; set; } = new HashSet<ProductTag>();
        public RatingSummary? RatingSummary { get; set; }
    }
}