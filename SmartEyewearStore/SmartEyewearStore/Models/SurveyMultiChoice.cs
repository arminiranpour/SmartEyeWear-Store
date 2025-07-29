using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEyewearStore.Models
{
    [Table("SURVEY_MULTI_CHOICES")]
    public class SurveyMultiChoice
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("SURVEY_ID")]
        public int SurveyId { get; set; }
        public SurveyAnswer Survey { get; set; }

        [Column("TYPE")]
        public string Type { get; set; }

        [Column("VALUE")]
        public string Value { get; set; }
    }
}