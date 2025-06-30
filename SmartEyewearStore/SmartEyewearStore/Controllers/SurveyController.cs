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

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(SurveyViewModel model)
    {
        if (ModelState.IsValid)
        {
            var survey = new SurveyAnswer
            {
                GlassType = model.GlassType.ToString(),
                Material = model.Material.ToString(),
                Gender = model.Gender.ToString(),
                Tone = model.Tone.ToString(),
                UserId = 1 // تستی
            };

            _context.SurveyAnswers.Add(survey);
            _context.SaveChanges();

            return RedirectToAction("Index", "Scan");
        }

        return View(model);
    }

    public IActionResult Success()
    {
        return View();
    }
}
