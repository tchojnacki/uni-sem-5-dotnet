using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using StoreApp.Models;

namespace StoreApp.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<Item> Items { get; set; } = new List<Item>();

        [Display(Name = "Sum")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice => Items.Sum(i => i.Price);

        public bool IsEmpty => !Items.Any();

        public class Item
        {
            public Article Article { get; set; } = default!;

            public int Count { get; set; }

            [DataType(DataType.Currency)]
            public decimal Price => Article.Price * Count;
        }
    }
}
