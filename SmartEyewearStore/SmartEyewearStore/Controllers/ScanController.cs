using Microsoft.AspNetCore.Mvc;
using SmartEyewearStore.Data;
using System.Linq;

public class ScanController : Controller
{
    private readonly ApplicationDbContext _context;

    public ScanController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SavePhoto(string faceShape, string skinTone)
    {
        var survey = _context.SurveyAnswers.OrderByDescending(x => x.Id).FirstOrDefault();
        if (survey != null)
        {
            survey.FaceShape = faceShape;
            survey.SkinTone = skinTone;

            _context.SurveyAnswers.Update(survey);
            _context.SaveChanges();
        }

        return RedirectToAction("Success", "Survey");
    }
}
