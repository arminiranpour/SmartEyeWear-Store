using System.Collections.Generic;

namespace SmartEyewearStore.ViewModels
{
    public class CatalogFiltersVM
    {
        // selected inputs via query string
        public int[]? BrandIds { get; set; }
        public int[]? ShapeIds { get; set; }
        public int[]? ColorIds { get; set; }
        public int[]? MaterialIds { get; set; }
        public int[]? RimStyleIds { get; set; }
        public int[]? FeatureIds { get; set; }
        public string[]? Sizes { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public string? Search { get; set; }
        public string? Sort { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        // options to render
        public IEnumerable<SelectOption> Brands { get; set; } = new List<SelectOption>();
        public IEnumerable<SelectOption> Shapes { get; set; } = new List<SelectOption>();
        public IEnumerable<SelectOption> Colors { get; set; } = new List<SelectOption>();
        public IEnumerable<SelectOption> Materials { get; set; } = new List<SelectOption>();
        public IEnumerable<SelectOption> RimStyles { get; set; } = new List<SelectOption>();
        public IEnumerable<SelectOption> Features { get; set; } = new List<SelectOption>();
        public IEnumerable<SelectOption> SizesOptions { get; set; } = new List<SelectOption>();
        public decimal PriceMinAvailable { get; set; }
        public decimal PriceMaxAvailable { get; set; }
    }
}