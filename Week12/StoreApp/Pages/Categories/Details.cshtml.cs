using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly StoreDbContext _context;

        public DetailsModel(StoreDbContext context) => _context = context;

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
    }
}
