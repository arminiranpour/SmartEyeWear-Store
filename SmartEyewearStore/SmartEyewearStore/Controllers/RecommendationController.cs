using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using SmartEyewearStore.Data;
using SmartEyewearStore.Services;
using SmartEyewearStore.Models;

namespace SmartEyewearStore.Controllers
{
    public class RecommendationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ContentBasedService _service;

        public RecommendationController(ApplicationDbContext context, ContentBasedService service)
        {
            _context = context;
            _service = service;
        }

        public IActionResult AnalyzeFromClient(SurveyViewModel model)
        {
            var survey = new SurveyAnswer
            {
                Gender = model.Gender.ToString(),
                Style = model.Style.ToString(),
                Lifestyle = model.Lifestyle.ToString(),
                BuyingFrequency = model.BuyingFrequency.ToString(),
                PriceFocus = model.PriceFocus.ToString(),
                FaceShape = model.FaceShape.ToString(),
                FavoriteShapes = model.FavoriteShapes,
                Colors = model.Colors,
                Materials = model.Materials,
                LensWidth = model.LensWidth,
                BridgeWidth = model.BridgeWidth,
                TempleLength = model.TempleLength,
                HeadSize = model.HeadSize.ToString(),
                ScreenTime = model.ScreenTime.ToString(),
                DayLocation = model.DayLocation.ToString(),
                Prescription = model.Prescription,
                Features = model.Features
            };

            var glasses = _context.Glasses
                .Include(g => g.GlassesInfo)
                .AsNoTracking()
                .ToList();

            var recommended = _service.GetRecommendedGlasses(survey, glasses);

            return View("GetRecommendations", recommended);
        }

        public IActionResult GetRecommendations()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Store");
            }

            var profile = _context.SurveyAnswers
                .Where(s => s.UserId == userId.Value)
                .OrderByDescending(s => s.Id)
                .FirstOrDefault();

            if (profile == null)
            {
                return RedirectToAction("Index", "Store");
            }

            var glasses = _context.Glasses
                .Include(g => g.GlassesInfo)
                .AsNoTracking()
                .ToList();

            var recommended = _service.GetRecommendedGlasses(profile, glasses);

            return View(recommended);
        }
    }
}