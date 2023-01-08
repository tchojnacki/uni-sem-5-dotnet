using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreApp.Models;
using System.Collections.Generic;
using System.Linq;
using StoreApp.Services;

namespace StoreApp.Pages.Shop
{
    public class CartModel : PageModel
    {
        private readonly ICartService _cartService;

        public CartModel(ICartService cartService) => _cartService = cartService;

        public IEnumerable<Item> Items { get; set; } = new List<Item>();

        public decimal TotalPrice => Items.Sum(i => i.Article.Price * i.Count);

        public bool IsEmpty => !Items.Any();

        public class Item
        {
            public Article Article { get; set; } = default!;
            public int Count { get; set; }
        }

        public void OnGet()
        {
            Items = _cartService
                .GetArticlesInCart()
                .Select(c => new Item { Article = c.Article, Count = c.Count });
        }
    }
}
