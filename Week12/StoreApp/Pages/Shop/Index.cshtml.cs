using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly StoreDbContext _context;

        public IndexModel(StoreDbContext context) => _context = context;

        [Display(Name = "Category")]
        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        public IEnumerable<Article> Articles { get; set; } = new List<Article>();

        public async Task<IActionResult> OnGet()
        {
            ViewData["Title"] = "Store";
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");

            if (CategoryId is { } id)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category is null)
                    return NotFound();

                ViewData["Title"] = category.Name;

                Articles = await _context.Articles.Where(a => a.CategoryId == id).ToListAsync();
            }

            return Page();
        }
    }
}
