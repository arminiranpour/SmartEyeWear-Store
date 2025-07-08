using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SmartEyewearStore.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult GetGuestId()
        {
            var guestId = HttpContext.Session.GetString("GuestId");
            return Json(new { guestId });
        }
    }
}