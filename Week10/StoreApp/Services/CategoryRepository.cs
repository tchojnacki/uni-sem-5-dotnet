using System.Collections.Generic;
using System.Linq;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Services
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly StoreDbContext _context;

        public CategoryRepository(StoreDbContext context) => _context = context;

        public IEnumerable<Category> All => _context.Categories.OrderBy(c => c.Id);

        public Category? this[int id] => _context.Categories.Find(id);

        public Category Add(Category category)
        {
            category.Id = default;
            _context.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Update(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
            return category;
        }

        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);

            if (category is null)
                return;

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
