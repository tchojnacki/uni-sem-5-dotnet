using Microsoft.EntityFrameworkCore;
using StoreApp.Models;

namespace StoreApp.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        public DbSet<Article> Articles => Set<Article>();

        public DbSet<Category> Categories => Set<Category>();
    }
}
