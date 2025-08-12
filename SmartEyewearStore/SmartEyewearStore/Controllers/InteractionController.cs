using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEyewearStore.Models;
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
        public IActionResult PostInteraction([FromBody] InteractionRequest request)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            string? guestId = HttpContext.Session.GetString("GuestId");

            if (userId != null)
            {
                guestId = null;
            }
            else if (string.IsNullOrEmpty(guestId))
            {
                guestId = Request.Query["guestId"].FirstOrDefault();
                if (string.IsNullOrEmpty(guestId))
                {
                    guestId = Guid.NewGuid().ToString();
                }
                HttpContext.Session.SetString("GuestId", guestId);
            }

            _interactionService.AddInteraction(request.VariantId, request.InteractionType, userId, guestId);
            return Ok();
        }
    }
}
