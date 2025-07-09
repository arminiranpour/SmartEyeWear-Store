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
        public bool? InStock { get; set; }
    }
}
