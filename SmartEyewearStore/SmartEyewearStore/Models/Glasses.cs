using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEyewearStore.Models
{
    [Table("GLASSES")]
    public class Glasses
    {
        public int Id { get; set; }
        public int GlassesInfoId { get; set; }
        public GlassesInfo GlassesInfo { get; set; }
        public string Color { get; set; }

        [Precision(10, 2)]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int? InStock { get; set; }

        [Precision(5, 2)]
        public decimal PopularityScore { get; set; } = 0;
        public int IsActive { get; set; } = 1;
    }
}
