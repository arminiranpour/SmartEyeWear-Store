using System.Collections.Generic;

namespace SmartEyewearStore.Models.Catalog
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<ProductTag> ProductTags { get; set; } = new HashSet<ProductTag>();
    }
}