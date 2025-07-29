using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEyewearStore.Models
{
    [Table("GLASSES_FEATURES")]
    public class GlassesFeature
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("GLASSESINFOID")]
        public int GlassesInfoId { get; set; }
        public GlassesInfo GlassesInfo { get; set; }

        [Column("FEATURE")]
        public string Feature { get; set; }
    }
}