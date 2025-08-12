using System;
using System.ComponentModel.DataAnnotations.Schema;
using SmartEyewearStore.Models.Catalog;
namespace SmartEyewearStore.Models
{
    [Table("USER_INTERACTIONS")]
    public class UserInteraction
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public string? GuestId { get; set; }
        public int VariantId { get; set; }
        public ProductVariant Variant { get; set; }
        public string InteractionType { get; set; }
        public int? Score { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}