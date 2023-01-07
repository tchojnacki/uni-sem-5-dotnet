using System.Collections.Generic;
using System.Linq;
using StoreApp.Models;

namespace StoreApp.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<Item> Items { get; set; } = new List<Item>();

        public decimal TotalPrice => Items.Sum(i => i.Article.Price * i.Count);
        public bool IsEmpty => !Items.Any();

        public class Item
        {
            public Article Article { get; set; } = default!;
            public int Count { get; set; }
        }
    }
}
