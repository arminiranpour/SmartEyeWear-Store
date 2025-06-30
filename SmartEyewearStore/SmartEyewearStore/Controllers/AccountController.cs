using Microsoft.AspNetCore.Mvc;
using SmartEyewearStore.Models;

namespace SmartEyewearStore.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: add authentication logic
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}