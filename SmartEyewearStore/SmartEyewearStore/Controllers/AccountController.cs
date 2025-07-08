using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using SmartEyewearStore.Data;
using SmartEyewearStore.Models;

namespace SmartEyewearStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

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
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    HttpContext.Session.SetInt32("UserId", user.Id);

                    var guestId = HttpContext.Session.GetString("GuestId");
                    if (!string.IsNullOrEmpty(guestId))
                    {
                        var interactions = _context.UserInteractions
                            .Where(x => x.GuestId == guestId && x.UserId == null)
                            .ToList();
                        foreach (var interaction in interactions)
                        {
                            interaction.UserId = user.Id;
                            interaction.GuestId = null;
                        }

                        var surveys = _context.SurveyAnswers
                            .Where(x => x.UserId == null)
                            .ToList();
                        foreach (var survey in surveys)
                        {
                            survey.UserId = user.Id;
                        }

                        _context.SaveChanges();
                        HttpContext.Session.Remove("GuestId");
                    }

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                HttpContext.Session.SetInt32("UserId", user.Id);

                var guestId = HttpContext.Session.GetString("GuestId");
                if (!string.IsNullOrEmpty(guestId))
                {
                    var interactions = _context.UserInteractions
                        .Where(x => x.GuestId == guestId && x.UserId == null)
                        .ToList();
                    foreach (var interaction in interactions)
                    {
                        interaction.UserId = user.Id;
                        interaction.GuestId = null;
                    }

                    var surveys = _context.SurveyAnswers
                        .Where(x => x.UserId == null)
                        .ToList();
                    foreach (var survey in surveys)
                    {
                        survey.UserId = user.Id;
                    }

                    _context.SaveChanges();
                    HttpContext.Session.Remove("GuestId");
                }

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}