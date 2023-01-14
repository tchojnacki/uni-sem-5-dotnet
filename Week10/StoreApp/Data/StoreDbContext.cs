using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreApp.Models;

namespace StoreApp.Data
{
    public class StoreDbContext : IdentityDbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        public DbSet<Article> Articles => Set<Article>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderArticle> OrderArticles => Set<OrderArticle>();

        public DbSet<DeliveryInfo> DeliveryInfos => Set<DeliveryInfo>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderArticle>().HasKey(c => new { c.ArticleId, c.OrderId });
        }
    }
}
