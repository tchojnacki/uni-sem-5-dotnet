using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly StoreDbContext _context;

        public IndexModel(StoreDbContext context) => _context = context;

        public IList<Article> Articles { get; set; } = new List<Article>();

        public async Task OnGetAsync()
        {
            Articles = await _context.Articles.Include(a => a.Category).ToListAsync();
        }
    }
}
