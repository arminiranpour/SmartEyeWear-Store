using System.Collections.Generic;

namespace SmartEyewearStore.Models.Catalog
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}