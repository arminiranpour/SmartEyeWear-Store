using Microsoft.AspNetCore.Mvc;
using SmartEyewearStore.Data;
using SmartEyewearStore.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
namespace SmartEyewearStore.Controllers
{
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
        public async Task<IActionResult> Analyze(IFormFile file)
        {
            var endpoint = "https://smarteyewearfaceapi.cognitiveservices.azure.com/";
            var key = "4TzH5BxHm8n01wF52qaxBKNmJxdtGpOK3H0LyYUDxqMjya8Y33mtJQQJ99BGACBsN54XJ3w3AAAKACOGhH5o";

            var faceHelper = new FaceApiHelper(endpoint, key);

            using var stream = file.OpenReadStream();
            var face = await faceHelper.DetectFaceAttributesAsync(stream);

            if (face != null)
            {
                var roll = face.FaceAttributes.HeadPose.Roll;
                var yaw = face.FaceAttributes.HeadPose.Yaw;

                string shape = "Oval";
                if (Math.Abs(yaw) > 20)
                    shape = "Round";
                else if (Math.Abs(roll) > 20)
                    shape = "Square";

                // پیدا کردن آخرین survey
                var survey = _context.SurveyAnswers.OrderByDescending(x => x.Id).FirstOrDefault();
                if (survey != null)
                {
                    survey.FaceShape = shape;
                    _context.SurveyAnswers.Update(survey);
                    await _context.SaveChangesAsync();
                }

                ViewBag.FaceShape = shape;
                ViewBag.Gender = face.FaceAttributes.Gender?.ToString();
                ViewBag.Age = face.FaceAttributes.Age?.ToString();

                return View("Index");
            }

            ViewBag.Error = "No face detected!";
            return View("Index");
        }
    }
}
