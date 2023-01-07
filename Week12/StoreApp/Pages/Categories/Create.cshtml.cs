using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly StoreDbContext _context;

        public CreateModel(StoreDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Category);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Categories/Index");
                }
            }
            catch
            {
                ModelState.AddModelError("Category.Name", "This Name is already taken.");
            }

            return Page();
        }
    }
}
