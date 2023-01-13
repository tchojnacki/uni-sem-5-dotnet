using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class CategoriesController : Controller
    {
        private readonly StoreDbContext _context;

        public CategoriesController(StoreDbContext context) => _context = context;

        public async Task<IActionResult> Index() => View(await _context.Categories.ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category is null)
                return NotFound();

            return View(category);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("Name", "This Name is already taken.");
            }

            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            var category = await _context.Categories.FindAsync(id);

            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
                return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
                {
                    return NotFound();
                }

                throw;
            }
            catch
            {
                ModelState.AddModelError("Name", "This Name is already taken.");
            }

            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            var articleCount = await _context.Articles.Where(a => a.CategoryId == id).CountAsync();
            if (articleCount > 0)
            {
                ModelState.AddModelError(
                    string.Empty,
                    $"This category has {articleCount} related articles. You can only delete categories with no articles."
                );
                return View(category);
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id) => _context.Categories.Any(e => e.Id == id);
    }
}
