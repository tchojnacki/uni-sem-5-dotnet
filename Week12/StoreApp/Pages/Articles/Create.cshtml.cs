using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreApp.Data;
using StoreApp.Models;
using StoreApp.Util;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace StoreApp.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly StoreDbContext _context;
        private readonly string _uploadPath;

        public CreateModel(StoreDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "upload");
        }

        [BindProperty]
        [MinLength(3, ErrorMessage = "The field {0} must not be shorter than {1}.")]
        [MaxLength(100, ErrorMessage = "The field {0} must not be longer than {1}.")]
        public string Name { get; set; } = default!;

        [BindProperty]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.PositiveInfinity, ErrorMessage = "The field {0} must be positive.")]
        public decimal Price { get; set; }

        [BindProperty]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [BindProperty]
        [AllowedExtension(".jpg")]
        public IFormFile? Photo { get; set; }

        public IActionResult OnGet()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Guid? photoGuid = null;
                if (Photo is { } photo)
                {
                    photoGuid = Guid.NewGuid();
                    await using var stream = System.IO.File.Create(PhotoPath(photoGuid.Value));
                    await photo.CopyToAsync(stream);
                }

                var article = new Article
                {
                    Name = Name,
                    Price = Price,
                    CategoryId = CategoryId,
                    PhotoGuid = photoGuid
                };

                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Articles/Index");
            }

            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", CategoryId);

            return Page();
        }

        private string PhotoPath(Guid guid) => Path.Join(_uploadPath, $"{guid}.jpg");
    }
}
