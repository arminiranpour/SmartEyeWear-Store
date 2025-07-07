namespace SmartEyewearStore.Models
{
    public class UserInteraction
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public string GuestId { get; set; }
        public int GlassId { get; set; }
        public Glasses Glass { get; set; }
        public string InteractionType { get; set; }
        public int? Score { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}