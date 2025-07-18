﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEyewearStore.Data;
using SmartEyewearStore.Models;

public class SurveyController : Controller
{
    private readonly ApplicationDbContext _context;

    public SurveyController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Survey
    public IActionResult Index()
    {
        return View();
    }

    // POST: /Survey
    [HttpPost]
    public IActionResult Index(SurveyViewModel model)
    {
        if (ModelState.IsValid)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
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
                    Features = string.IsNullOrEmpty(model.Features) ? "" : model.Features,
                    UserId = userId.Value
                };

                _context.SurveyAnswers.Add(survey);
                _context.SaveChanges();

                return RedirectToAction("GetHybridRecommendations", "Recommendation");
            }

            return View("SubmitHybrid", model);
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult Save([FromQuery] int userId, [FromBody] SurveyViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

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
            Features = string.IsNullOrEmpty(model.Features) ? "" : model.Features,
            UserId = userId
        };

        _context.SurveyAnswers.Add(survey);
        _context.SaveChanges();

        return Ok();
    }

    public IActionResult Success(SurveyViewModel model)
    {
        ViewData["UserId"] = HttpContext.Session.GetInt32("UserId");
        return View(model);
    }
}
