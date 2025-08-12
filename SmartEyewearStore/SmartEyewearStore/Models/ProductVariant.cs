using System.Collections.Generic;

namespace SmartEyewearStore.Models.Catalog
{
    public class ProductVariant
    {
        public int VariantId { get; set; }
        public int ProductId { get; set; }
        public int? ColorId { get; set; }
        public string? SizeLabel { get; set; }
        public string? Sku { get; set; }
        public bool? IsDefault { get; set; }

        public Product Product { get; set; } = null!;
        public Color? Color { get; set; }
        public VariantDimensions? Dimensions { get; set; }
        public VariantInventory? Inventory { get; set; }
        public ICollection<VariantImage> Images { get; set; } = new HashSet<VariantImage>();
        public ICollection<VariantPrice> Prices { get; set; } = new HashSet<VariantPrice>();
    }
}