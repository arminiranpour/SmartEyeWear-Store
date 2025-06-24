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
                FaceShape = model.FaceShape,
                PreferredColors = string.Join(",", model.PreferredColors),
                Style = model.Style,
                Usage = string.Join(",", model.Usage),
                EyeCondition = model.EyeCondition,
                Gender = model.Gender,
                AgeRange = model.AgeRange,
                BudgetRange = model.BudgetRange,
                UserId = 1 // تستی: بعداً با User واقعی جایگزین می‌کنیم
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
