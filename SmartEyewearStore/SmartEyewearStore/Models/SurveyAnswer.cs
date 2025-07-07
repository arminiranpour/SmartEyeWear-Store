using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEyewearStore.Models
{
    [Table("SURVEY_ANSWERS")]
    public class SurveyAnswer
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("GENDER")]
        public string Gender { get; set; }

        [Column("STYLE")]
        public string Style { get; set; }

        [Column("LIFESTYLE")]
        public string Lifestyle { get; set; }

        [Column("BUYING_FREQUENCY")]
        public string BuyingFrequency { get; set; }

        [Column("PRICE_FOCUS")]
        public string PriceFocus { get; set; }

        [Column("FACE_SHAPE")]
        public string FaceShape { get; set; }

        [Column("FAVORITE_SHAPES")]
        public string FavoriteShapes { get; set; }

        [Column("COLORS")]
        public string Colors { get; set; }

        [Column("MATERIALS")]
        public string Materials { get; set; }

        [Column("LENS_WIDTH")]
        public int? LensWidth { get; set; }

        [Column("BRIDGE_WIDTH")]
        public int? BridgeWidth { get; set; }

        [Column("TEMPLE_LENGTH")]
        public int? TempleLength { get; set; }

        [Column("HEAD_SIZE")]
        public string HeadSize { get; set; }

        [Column("SCREEN_TIME")]
        public string ScreenTime { get; set; }

        [Column("DAY_LOCATION")]
        public string DayLocation { get; set; }

        [Column("PRESCRIPTION")]
        public bool Prescription { get; set; }

        [Column("FEATURES")]
        public string Features { get; set; }

        [Column("USER_ID")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}