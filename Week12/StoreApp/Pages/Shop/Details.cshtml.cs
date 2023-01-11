using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Pages.Shop
{
    public class DetailsModel : PageModel
    {
        private readonly StoreDbContext _context;

        public DetailsModel(StoreDbContext context) => _context = context;

        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGet(int id)
        {
            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article is null)
                return NotFound();

            Article = article;
            return Page();
        }
    }
}
