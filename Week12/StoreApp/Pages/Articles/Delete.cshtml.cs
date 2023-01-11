using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Pages.Articles
{
    public class DeleteModel : PageModel
    {
        private readonly StoreDbContext _context;
        private readonly string _uploadPath;

        public DeleteModel(StoreDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "upload");
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article is null)
                return NotFound();

            Article = article;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article.PhotoGuid is { } photoGuid)
            {
                try
                {
                    System.IO.File.Delete(PhotoPath(photoGuid));
                }
                catch
                {
                    Debug.WriteLine("Error while deleting the file!");
                }
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Articles/Index");
        }

        private string PhotoPath(Guid guid) => Path.Join(_uploadPath, $"{guid}.jpg");
    }
}
