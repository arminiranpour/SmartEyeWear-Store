using System.Collections.Generic;

namespace SmartEyewearStore.Models.Catalog
{
    public class Color
    {
        public int ColorId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<ProductVariant> Variants { get; set; } = new HashSet<ProductVariant>();
    }
}