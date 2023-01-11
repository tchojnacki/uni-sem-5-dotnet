using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Services;
using StoreApp.ViewModels;

namespace StoreApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;

        public IActionResult Index()
        {
            var model = new CartViewModel
            {
                Items = _cartService
                    .GetAllItems()
                    .Select(c => new CartViewModel.Item { Article = c.Article, Count = c.Count })
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult IncrementArticleCount(int? id)
        {
            if (id is null)
                return NotFound();

            _cartService.IncrementArticleCount(id.Value);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DecrementArticleCount(int? id)
        {
            if (id is null)
                return NotFound();

            _cartService.DecrementArticleCount(id.Value);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemoveArticle(int? id)
        {
            if (id is null)
                return NotFound();

            _cartService.RemoveArticle(id.Value);

            return RedirectToAction(nameof(Index));
        }
    }
}
