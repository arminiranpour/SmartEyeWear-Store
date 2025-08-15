using System.Collections.Generic;

namespace SmartEyewearStore.ViewModels
{
    public class ProductCardVM
    {
        public int ProductId { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? BrandName { get; set; }
        public decimal MinPrice { get; set; }
        public string? PrimaryImageUrl { get; set; }
        public IEnumerable<string>? Colors { get; set; }
        public string? SizeLabel { get; set; }
    }
}