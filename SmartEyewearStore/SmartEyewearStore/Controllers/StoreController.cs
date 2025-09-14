using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEyewearStore.Data;
using SmartEyewearStore.Models.Catalog;

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
            var variants = _context.ProductVariants
                .Include(v => v.Product).ThenInclude(p => p.FrameSpecs).ThenInclude(fs => fs.Shape)
                .Include(v => v.Product).ThenInclude(p => p.ProductTags).ThenInclude(pt => pt.Tag)
                .Include(v => v.Color)
                .Include(v => v.Images)
                .Include(v => v.Prices)
                .AsNoTracking()
                .ToList();

            Console.WriteLine($"Loaded variant count (no join): {variants.Count}");
            return View(variants);
        }

        public IActionResult Shop()
        {
           
            return RedirectToAction("Index", "Shop");
        }

        public IActionResult Product(int id)
        {
            
            return View();
        }

    }
}