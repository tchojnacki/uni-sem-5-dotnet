using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreApp.Services;

namespace StoreApp.Pages.Cart
{
    public class IncrementArticleCountModel : PageModel
    {
        private readonly ICartService _cartService;

        public IncrementArticleCountModel(ICartService cartService) => _cartService = cartService;

        public IActionResult OnPost(int id)
        {
            _cartService.IncrementArticleCount(id);
            return RedirectToPage("/Cart/Index");
        }
    }
}
