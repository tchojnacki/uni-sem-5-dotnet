using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly StoreDbContext _context;

        public EditModel(StoreDbContext context) => _context = context;

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
                return NotFound();

            Category = category;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id != Category.Id)
                return BadRequest();

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(Category);
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("/Categories/Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.Id))
                    return NotFound();

                throw;
            }
            catch
            {
                ModelState.AddModelError("Category.Name", "This Name is already taken.");
            }

            return Page();
        }

        private bool CategoryExists(int id) => _context.Categories.Any(e => e.Id == id);
    }
}
