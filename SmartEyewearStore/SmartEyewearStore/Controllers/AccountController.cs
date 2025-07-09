using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using SmartEyewearStore.Data;
using SmartEyewearStore.Models;

namespace SmartEyewearStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static readonly JsonSerializerOptions _enumOptions = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        };
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
                    }
                    if (!string.IsNullOrEmpty(model.PendingSurvey))
                    {
                        try
                        {
                            var pending = JsonSerializer.Deserialize<SurveyViewModel>(model.PendingSurvey, _enumOptions); if (pending != null)
                            {
                                var survey = new SurveyAnswer
                                {
                                    Gender = pending.Gender.ToString(),
                                    Style = pending.Style.ToString(),
                                    Lifestyle = pending.Lifestyle.ToString(),
                                    BuyingFrequency = pending.BuyingFrequency.ToString(),
                                    PriceFocus = pending.PriceFocus.ToString(),
                                    FaceShape = pending.FaceShape.ToString(),
                                    FavoriteShapes = string.IsNullOrEmpty(pending.FavoriteShapes) ? string.Empty : pending.FavoriteShapes,
                                    Colors = string.IsNullOrEmpty(pending.Colors) ? string.Empty : pending.Colors,
                                    Materials = string.IsNullOrEmpty(pending.Materials) ? string.Empty : pending.Materials,
                                    LensWidth = pending.LensWidth,
                                    BridgeWidth = pending.BridgeWidth,
                                    TempleLength = pending.TempleLength,
                                    HeadSize = pending.HeadSize.ToString(),
                                    ScreenTime = pending.ScreenTime.ToString(),
                                    DayLocation = pending.DayLocation.ToString(),
                                    Prescription = pending.Prescription,
                                    Features = string.IsNullOrEmpty(pending.Features) ? string.Empty : pending.Features,
                                    UserId = user.Id
                                };
                                _context.SurveyAnswers.Add(survey);
                            }
                        }
                        catch { }
                    }

                    _context.SaveChanges();
                    HttpContext.Session.Remove("GuestId");

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
                }
                if (!string.IsNullOrEmpty(model.PendingSurvey))
                {
                    try
                    {
                        var pending = JsonSerializer.Deserialize<SurveyViewModel>(model.PendingSurvey, _enumOptions);
                        if (pending != null)
                        {
                            var survey = new SurveyAnswer
                            {
                                Gender = pending.Gender.ToString(),
                                Style = pending.Style.ToString(),
                                Lifestyle = pending.Lifestyle.ToString(),
                                BuyingFrequency = pending.BuyingFrequency.ToString(),
                                PriceFocus = pending.PriceFocus.ToString(),
                                FaceShape = pending.FaceShape.ToString(),
                                FavoriteShapes = string.IsNullOrEmpty(pending.FavoriteShapes) ? string.Empty : pending.FavoriteShapes,
                                Colors = string.IsNullOrEmpty(pending.Colors) ? string.Empty : pending.Colors,
                                Materials = string.IsNullOrEmpty(pending.Materials) ? string.Empty : pending.Materials,
                                LensWidth = pending.LensWidth,
                                BridgeWidth = pending.BridgeWidth,
                                TempleLength = pending.TempleLength,
                                HeadSize = pending.HeadSize.ToString(),
                                ScreenTime = pending.ScreenTime.ToString(),
                                DayLocation = pending.DayLocation.ToString(),
                                Prescription = pending.Prescription,
                                Features = string.IsNullOrEmpty(pending.Features) ? string.Empty : pending.Features,
                                UserId = user.Id
                            };
                            _context.SurveyAnswers.Add(survey);
                        }
                    }
                    catch { }
                }

                _context.SaveChanges();
                HttpContext.Session.Remove("GuestId");

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}