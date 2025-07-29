using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace SmartEyewearStore.Models
{
    [Table("GLASSES_INFO")]
    public class GlassesInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public string Shape { get; set; }
        public string Rim { get; set; }
        public string Style { get; set; }
        public string HeadSize { get; set; }
        public string Size { get; set; }
        public decimal? LensWidth { get; set; }
        public decimal? BridgeWidth { get; set; }
        public decimal? TempleLength { get; set; }
        public decimal? WeightGrams { get; set; }
        public string Material { get; set; }
        public string Fit { get; set; }
        public List<GlassesFeature> FeaturesList { get; set; } = new();
        public bool? HasAntiScratchCoating { get; set; }
    }
}