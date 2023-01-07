using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly StoreDbContext _context;

        public DeleteModel(StoreDbContext context) => _context = context;

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
                return NotFound();

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category is null)
                return NotFound();

            Category = category;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var category = await _context.Categories.FindAsync(id);

            var articleCount = await _context.Articles.Where(a => a.CategoryId == id).CountAsync();
            if (articleCount > 0)
            {
                ModelState.AddModelError(
                    string.Empty,
                    $"This category has {articleCount} related articles. You can only delete categories with no articles."
                );

                Category = category;
                return Page();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Categories/Index");
        }
    }
}
