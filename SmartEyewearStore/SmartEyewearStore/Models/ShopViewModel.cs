using Microsoft.AspNetCore.Mvc.Rendering;
using SmartEyewearStore.Models;
using SmartEyewearStore.Models.Catalog;
using System.Collections.Generic;

namespace SmartEyewearStore.Models
{
    /// <summary>
    /// View model used by the Shop view to render filter dropdowns and product listings.
    /// </summary>
    public class ShopViewModel
    {
        // Selected filter values
        public int? BrandId { get; set; }
        public int? SizeId { get; set; }
        public int? ShapeId { get; set; }
        public int? ColorId { get; set; }
        public int? MaterialId { get; set; }
        public int? RimStyleId { get; set; }
        public int? PriceRangeId { get; set; }
        public int? FeatureId { get; set; }

        // Items for filter dropdowns
        public IEnumerable<SelectListItem> BrandItems { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> SizeItems { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> ShapeItems { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> ColorItems { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> MaterialItems { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> RimItems { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> PriceRangeItems { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> FeatureItems { get; set; } = new List<SelectListItem>();

        // Products to display
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}