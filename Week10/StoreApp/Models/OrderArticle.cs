using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Models
{
    public class OrderArticle
    {
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int ArticleId { get; set; }
        public Article? Article { get; set; }

        [Range(1, int.MaxValue)]
        public int Count { get; set; }

        [NotMapped]
        [DataType(DataType.Currency)]
        public decimal Price => Count * Article?.Price ?? 0;
    }
}
