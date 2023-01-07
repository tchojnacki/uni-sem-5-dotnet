using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Pages.Articles
{
    public class EditModel : PageModel
    {
        private readonly StoreDbContext _context;

        public EditModel(StoreDbContext context) => _context = context;

        [BindProperty]
        public int Id { get; set; }

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
        public Guid? PhotoGuid { get; set; }

        public string Photo =>
            PhotoGuid is { } guid ? $"/upload/{guid}.jpg" : "/image/placeholder.jpg";

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
                return NotFound();

            var article = await _context.Articles.FindAsync(id);

            if (article is null)
                return NotFound();

            ViewData["Categories"] = new SelectList(
                _context.Categories,
                "Id",
                "Name",
                article.CategoryId
            );

            Id = article.Id;
            Name = article.Name;
            Price = article.Price;
            CategoryId = article.CategoryId;
            PhotoGuid = article.PhotoGuid;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id != Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var article = new Article
                    {
                        Id = Id,
                        Name = Name,
                        CategoryId = CategoryId,
                        PhotoGuid = PhotoGuid,
                        Price = Price
                    };
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToPage("/Articles/Index");
            }

            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", CategoryId);

            return Page();
        }

        private bool ArticleExists(int id) => _context.Articles.Any(e => e.Id == id);
    }
}
