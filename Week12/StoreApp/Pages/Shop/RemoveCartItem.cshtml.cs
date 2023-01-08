using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreApp.Services;

namespace StoreApp.Pages.Shop
{
    public class RemoveCartItemModel : PageModel
    {
        private readonly ICartService _cartService;

        public RemoveCartItemModel(ICartService cartService) => _cartService = cartService;

        public IActionResult OnPost(int? id)
        {
            if (id is null)
                return NotFound();

            _cartService.RemoveFromCart(id.Value);

            return RedirectToPage("/Shop/Cart");
        }
    }
}
