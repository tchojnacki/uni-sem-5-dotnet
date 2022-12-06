using System;

namespace StoreApp.Models
{
    public enum ArticleCategory
    {
        Example
    }

    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public ArticleCategory Category { get; set; }
    }
}
