using System.ComponentModel.DataAnnotations;

namespace SmartEyewearStore.Models
{
    public enum GenderEnum { Male, Female, Unisex }
    public enum StyleEnum { Artsy, Bold, Classic, StreetStyle, Sporty, Retro }
    public enum LifestyleEnum { Technology, Fashion, Fitness, Outdoors, EcoConscious }
    public enum BuyingFrequencyEnum { OnceAMonth, EverySixMonths, OnceAYear, EveryTwoYears }
    public enum PriceFocusEnum { Affordability, BestValue, Premium }
    public enum FaceShapeEnum { Triangle, Oval, Square, Round, Heart, Diamond }
    public enum HeadSizeEnum { Narrow, Medium, Wide }
    public enum ScreenTimeEnum { Zero, ZeroToFour, FourToEight, EightPlus }
    public enum DayLocationEnum { AlwaysIndoors, MostlyIndoors, Both, MostlyOutdoors, AlwaysOutdoors }

    public class SurveyViewModel
    {
        [Required]
        public GenderEnum Gender { get; set; }

        [Required]
        public StyleEnum Style { get; set; }

        [Required]
        public LifestyleEnum Lifestyle { get; set; }

        [Required]
        public BuyingFrequencyEnum BuyingFrequency { get; set; }

        [Required]
        public PriceFocusEnum PriceFocus { get; set; }

        [Required]
        public FaceShapeEnum FaceShape { get; set; }

        [Required]
        public string FavoriteShapes { get; set; }

        [Required]
        public string Colors { get; set; }

        [Required]
        public string Materials { get; set; }

        public int? LensWidth { get; set; }
        public int? BridgeWidth { get; set; }
        public int? TempleLength { get; set; }

        [Required]
        public HeadSizeEnum HeadSize { get; set; }

        [Required]
        public ScreenTimeEnum ScreenTime { get; set; }

        [Required]
        public DayLocationEnum DayLocation { get; set; }

        [Required]
        public bool Prescription { get; set; }

        [Required]
        public string Features { get; set; }
    }
}