using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SmartEyewearStore.Services;

namespace SmartEyewearStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InteractionController : Controller
    {
        private readonly InteractionService _interactionService;

        public InteractionController(InteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        [HttpPost]
        public IActionResult PostInteraction(int glassId, string interactionType)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            string? guestId = HttpContext.Session.GetString("GuestId");

            if (userId == null && string.IsNullOrEmpty(guestId))
            {
                guestId = Request.Query["guestId"].FirstOrDefault();
            }

            _interactionService.AddInteraction(glassId, interactionType, userId, guestId);
            return Ok();
        }
    }
}