namespace SmartEyewearStore.Models
{
    public class SurveyAnswer
    {
        public int Id { get; set; }
        public string FaceShape { get; set; }
        public string PreferredColors { get; set; }
        public string Style { get; set; }
        public string Usage { get; set; }
        public bool EyeCondition { get; set; }
        public string Gender { get; set; }
        public string AgeRange { get; set; }
        public string BudgetRange { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
