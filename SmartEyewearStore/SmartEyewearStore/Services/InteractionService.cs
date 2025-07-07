using SmartEyewearStore.Data;
using SmartEyewearStore.Models;

namespace SmartEyewearStore.Services
{
    public class InteractionService
    {
        private readonly ApplicationDbContext _context;

        public InteractionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddInteraction(int glassId, string interactionType, int? userId = null, string? guestId = null)
        {
            int score = interactionType switch
            {
                "Click" => 1,
                "Favorite" => 2,
                "AddToCart" => 2,
                "Purchase" => 3,
                _ => 0
            };

            var interaction = new UserInteraction
            {
                UserId = userId,
                GuestId = guestId,
                GlassId = glassId,
                InteractionType = interactionType,
                Score = score,
                Timestamp = DateTime.UtcNow
            };

            _context.UserInteractions.Add(interaction);
            _context.SaveChanges();
        }
    }
}