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
            var conn = _context.Database.GetDbConnection();
            Console.WriteLine($"Connection string being used: {conn.ConnectionString}");

        }

        public IActionResult Index()
        {
            var glasses = _context.Glasses
            .Where(g => g.IsActive)
            .AsNoTracking()
            .ToList();

            Console.WriteLine($"Loaded glasses count (no join): {glasses.Count}");
            return View(glasses);
        }
    }
}