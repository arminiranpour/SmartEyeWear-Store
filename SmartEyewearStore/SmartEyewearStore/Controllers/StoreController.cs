using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using SmartEyewearStore.Data;

namespace SmartEyewearStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Store
        public IActionResult Index()
        {
            var glasses = _context.Glasses
                .Include(g => g.GlassesInfo)
                .AsNoTracking()
                .ToList();
            ViewData["UserId"] = HttpContext.Session.GetInt32("UserId");
            return View(glasses);
        }

        // GET: /Store/TryOn/5
        public IActionResult TryOn(int id)
        {
            var glass = _context.Glasses
                .Include(g => g.GlassesInfo)
                .AsNoTracking()
                .FirstOrDefault(g => g.Id == id);
            if (glass == null)
            {
                return NotFound();
            }
            return View(glass);
        }
    }
}