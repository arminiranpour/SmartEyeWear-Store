using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using SmartEyewearStore.Data;
using SmartEyewearStore.Helpers;
using SmartEyewearStore.Models;

namespace SmartEyewearStore.Controllers
{
    public class ScanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly FaceApiHelper _faceApi;

        public ScanController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            var endpoint = config["FaceApi:Endpoint"] ?? string.Empty;
            var key = config["FaceApi:Key"] ?? string.Empty;
            _faceApi = new FaceApiHelper(endpoint, key);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.SurveyId = id;
            var survey = await _context.SurveyAnswers.FindAsync(id);
            return View(survey);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "تصویر معتبری انتخاب نشده است.");
                ViewBag.SurveyId = id;
                var surveyMissing = await _context.SurveyAnswers.FindAsync(id);
                return View(surveyMissing);
            }

            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);

            try
            {
                var result = await _faceApi.AnalyzeFaceAsync(ms);
                var survey = await _context.SurveyAnswers.FindAsync(id);
                if (survey != null)
                {
                    survey.FaceShape = result.faceShape;
                    survey.SkinTone = result.skinTone;
                    await _context.SaveChangesAsync();
                }
                ViewBag.SurveyId = id;
                return View(survey);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.SurveyId = id;
                var survey = await _context.SurveyAnswers.FindAsync(id);
                return View(survey);
            }
        }

    }
}