using System.ComponentModel.DataAnnotations;


namespace SmartEyewearStore.Models
{
    public class InteractionRequest
    {
        [Required]
        public int VariantId { get; set; }
        [Required]
        public string InteractionType { get; set; } = string.Empty;
    }
}