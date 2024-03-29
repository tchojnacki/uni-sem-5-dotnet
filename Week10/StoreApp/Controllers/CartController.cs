﻿using Microsoft.AspNetCore.Mvc;
using StoreApp.Data;
using StoreApp.Services;
using StoreApp.Util;

namespace StoreApp.Controllers
{
    [Deny(Roles = Role.Admin)]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;

        public IActionResult Index() => View(_cartService.Cart);

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
