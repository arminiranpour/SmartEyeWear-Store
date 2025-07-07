using System.ComponentModel.DataAnnotations;

namespace SmartEyewearStore.Models
{
    public class InteractionRequest
    {
        [Required]
        public int GlassId { get; set; }

        [Required]
        public string InteractionType { get; set; } = string.Empty;
    }
}