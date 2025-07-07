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
                Features = model.Features,
                UserId = 1 // TODO: replace with real user id
            };

            _context.SurveyAnswers.Add(survey);
            _context.SaveChanges();
            return RedirectToAction("Success");
        }

        return View(model);
    }

    public IActionResult Success()
    {
        return View();
    }
}
