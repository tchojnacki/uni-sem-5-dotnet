using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreApp.Services;

namespace StoreApp.Pages.Cart
{
    public class DecrementArticleCountModel : PageModel
    {
        private readonly ICartService _cartService;

        public DecrementArticleCountModel(ICartService cartService) => _cartService = cartService;

        public IActionResult OnPost(int id)
        {
            _cartService.DecrementArticleCount(id);
            return RedirectToPage("/Cart/Index");
        }
    }
}
