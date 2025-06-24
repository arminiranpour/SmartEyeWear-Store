using System.ComponentModel.DataAnnotations;

public class SurveyViewModel
{
    [Required]
    public string FaceShape { get; set; }

    [Required]
    public List<string> PreferredColors { get; set; }

    [Required]
    public string Style { get; set; }

    [Required]
    public List<string> Usage { get; set; }

    public bool EyeCondition { get; set; }

    [Required]
    public string Gender { get; set; }

    [Required]
    public string AgeRange { get; set; }

    [Required]
    public string BudgetRange { get; set; }
}
