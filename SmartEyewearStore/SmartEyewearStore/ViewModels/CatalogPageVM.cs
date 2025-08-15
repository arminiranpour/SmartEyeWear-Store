using System.Collections.Generic;

namespace SmartEyewearStore.ViewModels
{
    public class CatalogPageVM
    {
        public List<ProductCardVM> Items { get; set; } = new List<ProductCardVM>();
        public CatalogFiltersVM Filters { get; set; } = new CatalogFiltersVM();
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
