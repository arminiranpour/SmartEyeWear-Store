using Microsoft.AspNetCore.Mvc;
using SmartEyewearStore.Data;
using SmartEyewearStore.Models;
using System;

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
            try
            {
                var survey = new SurveyAnswer
                {
                    GlassType = model.GlassType.ToString(),
                    Material = model.Material.ToString(),
                    Gender = model.Gender.ToString(),
                    Tone = model.Tone.ToString(),
                    UserId = 1 // تستی — بعداً کاربر واقعی جایگزین می‌شود
                };

                _context.SurveyAnswers.Add(survey);
                _context.SaveChanges();

                // ریدایرکت به صفحه اسکن همراه با شناسه پاسخ ثبت شده
                return RedirectToAction("Index", "Scan", new { id = survey.Id });
            }
            catch (Exception ex)
            {
                // نمایش خطا روی صفحه
                return Content("❌ خطا هنگام ذخیره: " + ex.Message);
            }
        }

        // اگر ModelState معتبر نباشد، دوباره فرم نمایش داده شود
        return View(model);
    }

    public IActionResult Success()
    {
        return View();
    }
}
