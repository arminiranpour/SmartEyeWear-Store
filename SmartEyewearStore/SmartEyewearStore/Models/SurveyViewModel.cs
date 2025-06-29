using System.ComponentModel.DataAnnotations;

public class SurveyViewModel
{
    public enum GlassTypeEnum
    {
        Sunglasses,
        Eyeglasses
    }

    public enum MaterialEnum
    {
        Plastic_acetate,
        Metallic
    }

    public enum GenderEnum
    {
        Masculine,
        Feminine
    }

    public enum ToneEnum
    {
        Dark,
        Colorful,
        Neutral
    }

    public class SurveyViewModel
    {
        [Required]
        public GlassTypeEnum GlassType { get; set; }

        [Required]
        public MaterialEnum Material { get; set; }

        [Required]
        public GenderEnum Gender { get; set; }

        [Required]
        public ToneEnum Tone { get; set; }
    }

}
