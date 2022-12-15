using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.ViewModels;

namespace StoreApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly StoreDbContext _context;

        public ShopController(StoreDbContext context) => _context = context;

        public async Task<IActionResult> Index(StoreIndexViewModel model)
        {
            ViewBag.Title = "Store";
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            if (model.CategoryId is { } id)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category is null)
                    return NotFound();

                ViewBag.Title = category.Name;

                model.Articles = await _context.Articles
                    .Where(a => a.CategoryId == id)
                    .ToListAsync();
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article is null)
                return NotFound();

            return View(article);
        }
    }
}
