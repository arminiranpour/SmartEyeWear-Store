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