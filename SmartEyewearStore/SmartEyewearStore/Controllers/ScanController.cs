using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SmartEyewearStore.Data;
using SmartEyewearStore.Models;

namespace SmartEyewearStore.Controllers
{
    public class ScanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public ScanController(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
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

            var client = _httpClientFactory.CreateClient();
            string faceShape = "Unknown";

            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            var bytes = ms.ToArray();

            using var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(bytes), "image", "upload.jpg");

            try
            {
                var response = await client.PostAsync("http://127.0.0.1:5000/analyze", content);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    faceShape = result.face_shape;
                }
            }
            catch
            {
                faceShape = "Error";
            }

            var survey = await _context.SurveyAnswers.FindAsync(id);
            if (survey != null)
            {
                survey.FaceShape = faceShape;
                await _context.SaveChangesAsync();
            }

            ViewBag.SurveyId = id;
            return View(survey);
        }
    }
}
