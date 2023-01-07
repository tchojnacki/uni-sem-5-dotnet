using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly StoreDbContext _context;

        public IndexModel(StoreDbContext context) => _context = context;

        public IList<Category> Categories { get; private set; } = new List<Category>();

        public async Task OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
        }
    }
}
