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
    [HttpGet]
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
                GlassType = model.GlassType.ToString(),
                Material = model.Material.ToString(),
                Gender = model.Gender.ToString(),
                Tone = model.Tone.ToString(),
                UserId = 1 // تستی — بعداً با User واقعی جایگزین می‌کنیم
            };

            _context.SurveyAnswers.Add(survey);
            _context.SaveChanges();

            return RedirectToAction("Success");
        }

        return View(model);
    }

    // GET: /Survey/Success
    public IActionResult Success()
    {
        return View();
    }
}
