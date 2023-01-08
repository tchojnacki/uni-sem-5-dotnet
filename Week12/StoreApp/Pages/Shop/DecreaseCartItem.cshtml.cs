using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreApp.Services;

namespace StoreApp.Pages.Shop
{
    public class DecreaseCartItemModel : PageModel
    {
        private readonly ICartService _cartService;

        public DecreaseCartItemModel(ICartService cartService) => _cartService = cartService;

        public IActionResult OnPost(int? id)
        {
            if (id is null)
                return NotFound();

            _cartService.DecreaseInCart(id.Value);

            return RedirectToPage("/Shop/Cart");
        }
    }
}
