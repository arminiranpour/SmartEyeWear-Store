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
        private readonly CollaborativeFilteringService _collabService;

        public RecommendationController(ApplicationDbContext context, ContentBasedService service, CollaborativeFilteringService collabService)
        {
            _context = context;
            _service = service;
            _collabService = collabService;
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
                LensWidth = model.LensWidth.HasValue ? model.LensWidth : 0,
                BridgeWidth = model.BridgeWidth.HasValue ? model.BridgeWidth : 0,
                TempleLength = model.TempleLength.HasValue ? model.TempleLength : 0,
                HeadSize = model.HeadSize.ToString(),
                ScreenTime = model.ScreenTime.ToString(),
                DayLocation = model.DayLocation.ToString(),
                Prescription = model.Prescription
            };
            void AddChoices(string type, string raw)
            {
                if (string.IsNullOrWhiteSpace(raw)) return;
                foreach (var v in raw.Split(',', System.StringSplitOptions.RemoveEmptyEntries))
                {
                    survey.MultiChoices.Add(new SurveyMultiChoice { Type = type, Value = v.Trim() });
                }
            }

            AddChoices("shape", model.FavoriteShapes);
            AddChoices("color", model.Colors);
            AddChoices("material", model.Materials);
            AddChoices("feature", model.Features);

            var glasses = _context.Glasses
                .Where(g => g.IsActive)
                .Include(g => g.GlassesInfo)
                .ThenInclude(i => i.FeaturesList)
                .AsNoTracking()
                .ToList();

            var interactions = LoadInteractions();

            var recommended = _service.GetRecommendedGlasses(survey, glasses, interactions);

            return View("GetRecommendations", recommended);
        }

        [HttpPost]
        public IActionResult AnalyzeHybridFromClient(SurveyViewModel model)
        {
            var survey = new SurveyAnswer
            {
                Gender = model.Gender.ToString(),
                Style = model.Style.ToString(),
                Lifestyle = model.Lifestyle.ToString(),
                BuyingFrequency = model.BuyingFrequency.ToString(),
                PriceFocus = model.PriceFocus.ToString(),
                FaceShape = model.FaceShape.ToString(),
                LensWidth = model.LensWidth.HasValue ? model.LensWidth : 0,
                BridgeWidth = model.BridgeWidth.HasValue ? model.BridgeWidth : 0,
                TempleLength = model.TempleLength.HasValue ? model.TempleLength : 0,
                HeadSize = model.HeadSize.ToString(),
                ScreenTime = model.ScreenTime.ToString(),
                DayLocation = model.DayLocation.ToString(),
                Prescription = model.Prescription,
            };
            void AddChoices(string type, string raw)
            {
                if (string.IsNullOrWhiteSpace(raw)) return;
                foreach (var v in raw.Split(',', System.StringSplitOptions.RemoveEmptyEntries))
                {
                    survey.MultiChoices.Add(new SurveyMultiChoice { Type = type, Value = v.Trim() });
                }
            }

            AddChoices("shape", model.FavoriteShapes);
            AddChoices("color", model.Colors);
            AddChoices("material", model.Materials);
            AddChoices("feature", model.Features);

            var allGlasses = _context.Glasses
               .Where(g => g.IsActive)
               .Include(g => g.GlassesInfo)
                   .ThenInclude(i => i.FeaturesList)
               .AsNoTracking()
               .ToList();

            var allInteractions = LoadAllInteractions();

            var hybridService = HttpContext.RequestServices.GetService<HybridRecommendationService>();
            var recommended = hybridService.GetHybridRecommendationsWithScores(
                survey,
                allInteractions,
                allGlasses,
                _service,
                _collabService);

            return View("GetHybridRecommendations", recommended);
        }

        public IActionResult GetRecommendations()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Store");
            }

            var profile = _context.SurveyAnswers
                .Include(s => s.MultiChoices)
                .Where(s => s.UserId == userId.Value)
                .OrderByDescending(s => s.Id)
                .FirstOrDefault();

            if (profile == null)
            {
                return RedirectToAction("Index", "Store");
            }

            var glasses = _context.Glasses
                .Where(g => g.IsActive)
                .Include(g => g.GlassesInfo)
                    .ThenInclude(i => i.FeaturesList)
                .AsNoTracking()
                .ToList();

            var interactions = LoadInteractions();

            var recommended = _service.GetRecommendedGlasses(profile, glasses, interactions);

            return View(recommended);
        }

        public IActionResult GetCollaborativeRecommendations()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            string? guestId = HttpContext.Session.GetString("GuestId");

            string? targetKey = userId?.ToString() ?? guestId;
            if (string.IsNullOrEmpty(targetKey))
            {
                return RedirectToAction("Index", "Store");
            }

            var allInteractions = LoadAllInteractions();
            var topUsers = _collabService.GetTopSimilarUsers(targetKey, allInteractions);
            var recommendedIds = _collabService.GetRecommendedGlassIds(targetKey, allInteractions, topUsers);

            var glasses = _context.Glasses
                .Where(g => g.IsActive)
                .Include(g => g.GlassesInfo)
                    .ThenInclude(i => i.FeaturesList)
                .AsNoTracking()
                .Where(g => recommendedIds.Contains(g.Id))
                .ToList();

            var ordered = recommendedIds
                .Join(glasses, id => id, g => g.Id, (id, g) => g)
                .ToList();

            return View(ordered);
        }

        public IActionResult GetHybridRecommendations()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            string? guestId = HttpContext.Session.GetString("GuestId");

            string? targetKey = userId?.ToString() ?? guestId;
            if (string.IsNullOrEmpty(targetKey))
            {
                return RedirectToAction("Index", "Store");
            }

            var profile = _context.SurveyAnswers
                .Include(s => s.MultiChoices)
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.Id)
                .FirstOrDefault();

            if (profile == null)
            {
                return RedirectToAction("Index", "Store");
            }

            var allGlasses = _context.Glasses
                .Where(g => g.IsActive)
                .Include(g => g.GlassesInfo)
                    .ThenInclude(i => i.FeaturesList)
                .AsNoTracking()
                .ToList();

            var allInteractions = LoadAllInteractions();

            var hybridService = HttpContext.RequestServices.GetService<HybridRecommendationService>();
            var recommended = hybridService.GetHybridRecommendationsWithScores(
                profile,
                allInteractions,
                allGlasses,
                _service,
                _collabService);

            return View(recommended);
        }

        private List<UserInteraction> LoadInteractions()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            string? guestId = HttpContext.Session.GetString("GuestId");

            var query = _context.UserInteractions
                .Include(ui => ui.Glass)
                    .ThenInclude(g => g.GlassesInfo)
                        .ThenInclude(i => i.FeaturesList)
                .AsNoTracking()
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(ui => ui.UserId == userId.Value);
            }
            else if (!string.IsNullOrEmpty(guestId))
            {
                query = query.Where(ui => ui.GuestId == guestId);
            }
            else
            {
                return new List<UserInteraction>();
            }

            return query.ToList();
        }

        private List<UserInteraction> LoadAllInteractions()
        {
            return _context.UserInteractions
                .Include(ui => ui.Glass)
                    .ThenInclude(g => g.GlassesInfo)
                        .ThenInclude(i => i.FeaturesList)
                .AsNoTracking()
                .ToList();
        }
    }
}
