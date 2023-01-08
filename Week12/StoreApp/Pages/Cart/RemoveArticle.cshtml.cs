using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreApp.Services;

namespace StoreApp.Pages.Cart
{
    public class RemoveArticleModel : PageModel
    {
        private readonly ICartService _cartService;

        public RemoveArticleModel(ICartService cartService) => _cartService = cartService;

        public IActionResult OnPost(int id)
        {
            _cartService.RemoveArticle(id);
            return RedirectToPage("/Cart/Index");
        }
    }
}
