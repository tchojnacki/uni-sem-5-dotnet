using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreApp.Services;

namespace StoreApp.Pages.Shop
{
    public class AddToCartModel : PageModel
    {
        private readonly ICartService _cartService;

        public AddToCartModel(ICartService cartService) => _cartService = cartService;

        public IActionResult OnPost(int? id)
        {
            if (id is null)
                return NotFound();

            _cartService.IncreaseInCart(id.Value);

            return RedirectToPage("/Shop/Cart");
        }
    }
}
