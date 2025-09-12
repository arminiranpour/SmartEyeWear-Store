using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEyewearStore.Data;
using SmartEyewearStore.Models;
using SmartEyewearStore.Models.Catalog;
using SmartEyewearStore.Services;

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
                FavoriteShapes = string.IsNullOrEmpty(model.FavoriteShapes) ? "" : model.FavoriteShapes,
                Colors = string.IsNullOrEmpty(model.Colors) ? "" : model.Colors,
                Materials = string.IsNullOrEmpty(model.Materials) ? "" : model.Materials,
                LensWidth = model.LensWidth.HasValue ? model.LensWidth : 0,
                BridgeWidth = model.BridgeWidth.HasValue ? model.BridgeWidth : 0,
                TempleLength = model.TempleLength.HasValue ? model.TempleLength : 0,
                HeadSize = model.HeadSize.ToString(),
                ScreenTime = model.ScreenTime.ToString(),
                DayLocation = model.DayLocation.ToString(),
                Prescription = model.Prescription,
                Features = string.IsNullOrEmpty(model.Features) ? "" : model.Features
            };

            var variants = LoadAllVariants();


            var interactions = LoadInteractions();

            var recommended = _service.GetRecommendedVariants(survey, variants, interactions);

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
                FavoriteShapes = string.IsNullOrEmpty(model.FavoriteShapes) ? "" : model.FavoriteShapes,
                Colors = string.IsNullOrEmpty(model.Colors) ? "" : model.Colors,
                Materials = string.IsNullOrEmpty(model.Materials) ? "" : model.Materials,
                LensWidth = model.LensWidth.HasValue ? model.LensWidth : 0,
                BridgeWidth = model.BridgeWidth.HasValue ? model.BridgeWidth : 0,
                TempleLength = model.TempleLength.HasValue ? model.TempleLength : 0,
                HeadSize = model.HeadSize.ToString(),
                ScreenTime = model.ScreenTime.ToString(),
                DayLocation = model.DayLocation.ToString(),
                Prescription = model.Prescription,
                Features = string.IsNullOrEmpty(model.Features) ? "" : model.Features
            };

            var allVariants = LoadAllVariants();


            var allInteractions = LoadAllInteractions();

            var hybridService = HttpContext.RequestServices.GetService<HybridRecommendationService>();
            var recommended = hybridService.GetHybridRecommendationsWithScores(
                survey,
                allInteractions,
                allVariants,
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
                .Where(s => s.UserId == userId.Value)
                .OrderByDescending(s => s.Id)
                .FirstOrDefault();

            if (profile == null)
            {
                return RedirectToAction("Index", "Store");
            }

            var variants = LoadAllVariants();


            var interactions = LoadInteractions();

            var recommended = _service.GetRecommendedVariants(profile, variants, interactions);

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
            var recommendedIds = _collabService.GetRecommendedVariantIds(targetKey, allInteractions, topUsers);

            var variants = LoadAllVariants()
                .Where(v => recommendedIds.Contains(v.VariantId))
                .ToList();

            var ordered = recommendedIds
                .Join(variants, id => id, v => v.VariantId, (id, v) => v)
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
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.Id)
                .FirstOrDefault();

            // If a logged in user has not taken the survey yet, redirect them to it
            if (profile == null && userId.HasValue)
            {
                return RedirectToAction("Index", "Survey");
            }

            profile ??= new SurveyAnswer { UserId = userId ?? 0 };

            var allVariants = LoadAllVariants();


            var allInteractions = LoadAllInteractions();

            var hybridService = HttpContext.RequestServices.GetService<HybridRecommendationService>();
            var recommended = hybridService.GetHybridRecommendationsWithScores(
                profile,
                allInteractions,
                allVariants,
                _service,
                _collabService);

            return View(recommended);
        }

        private List<ProductVariant> LoadAllVariants()
        {
            return _context.ProductVariants
                .Include(v => v.Product)
                    .ThenInclude(p => p.FrameSpecs)
                        .ThenInclude(fs => fs.Shape)
                .Include(v => v.Product)
                    .ThenInclude(p => p.FrameSpecs)
                        .ThenInclude(fs => fs.Material)
                .Include(v => v.Product)
                    .ThenInclude(p => p.ProductTags)
                        .ThenInclude(pt => pt.Tag)
                .Include(v => v.Product)
                    .ThenInclude(p => p.ProductFeatures)
                        .ThenInclude(pf => pf.Feature)
                .Include(v => v.Color)
                .AsNoTracking()
                .ToList();
        }
        private List<UserInteraction> LoadInteractions()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            string? guestId = HttpContext.Session.GetString("GuestId");

            var query = _context.UserInteractions
                                .Include(ui => ui.Variant)
                    .ThenInclude(v => v.Product)
                        .ThenInclude(p => p.FrameSpecs)
                            .ThenInclude(fs => fs.Shape)
                .Include(ui => ui.Variant)
                    .ThenInclude(v => v.Product)
                        .ThenInclude(p => p.FrameSpecs)
                            .ThenInclude(fs => fs.Material)
                .Include(ui => ui.Variant)
                    .ThenInclude(v => v.Product)
                        .ThenInclude(p => p.ProductTags)
                            .ThenInclude(pt => pt.Tag)
                .Include(ui => ui.Variant)
                    .ThenInclude(v => v.Product)
                        .ThenInclude(p => p.ProductFeatures)
                            .ThenInclude(pf => pf.Feature)
                .Include(ui => ui.Variant)
                    .ThenInclude(v => v.Color)
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
                .Include(ui => ui.Variant)
                    .ThenInclude(v => v.Product)
                        .ThenInclude(p => p.FrameSpecs)
                            .ThenInclude(fs => fs.Shape)
                .Include(ui => ui.Variant)
                    .ThenInclude(v => v.Product)
                        .ThenInclude(p => p.FrameSpecs)
                            .ThenInclude(fs => fs.Material)
                .Include(ui => ui.Variant)
                    .ThenInclude(v => v.Product)
                        .ThenInclude(p => p.ProductTags)
                            .ThenInclude(pt => pt.Tag)
                .Include(ui => ui.Variant)
                    .ThenInclude(v => v.Product)
                        .ThenInclude(p => p.ProductFeatures)
                            .ThenInclude(pf => pf.Feature)
                .Include(ui => ui.Variant)
                    .ThenInclude(v => v.Color)
                .AsNoTracking()
                .ToList();
        }
    }
}
