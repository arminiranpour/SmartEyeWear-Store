using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEyewearStore.Data;
using SmartEyewearStore.Models;

namespace SmartEyewearStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var glasses = _context.Glasses
                .Include(g => g.GlassesInfo)
                .ToList();
            return View(glasses);
        }
    }
}