namespace SmartEyewearStore.Models
{
    public class SurveyAnswer
    {
        public int Id { get; set; }

        public string GlassType { get; set; }

        public string Material { get; set; }

        public string Gender { get; set; }

        public string Tone { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
