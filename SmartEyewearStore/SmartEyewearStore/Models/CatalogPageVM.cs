using System.Collections.Generic;

namespace SmartEyewearStore.ViewModels
{
    public class CatalogPageVM
    {
        public List<ProductCardVM> Items { get; set; } = new();
        public CatalogFiltersVM Filters { get; set; } = new();
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}