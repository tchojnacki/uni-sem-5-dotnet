namespace StoreApp.DTOs
{
    public class ArticleDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = default!;

        public string Photo { get; set; } = default!;
    }
}
