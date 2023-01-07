using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;
using StoreApp.Util;
using StoreApp.ViewModels;

namespace StoreApp.Controllers
{
    public class ShopController : Controller
    {
        private const string CartCookiePrefix = "article";
        private readonly StoreDbContext _context;

        public ShopController(StoreDbContext context) => _context = context;

        public async Task<IActionResult> Index(StoreIndexViewModel model)
        {
            ViewBag.Title = "Store";
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            if (model.CategoryId is { } id)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category is null)
                    return NotFound();

                ViewBag.Title = category.Name;

                model.Articles = await _context.Articles
                    .Where(a => a.CategoryId == id)
                    .ToListAsync();
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article is null)
                return NotFound();

            return View(article);
        }

        public IActionResult Cart()
        {
            var model = new CartViewModel
            {
                Items = CookieGetArticles()
                    .Select(c => new CartViewModel.Item { Article = c.Article, Count = c.Count })
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddToCart(int? id)
        {
            if (id is null)
                return NotFound();

            CookieAddToCart(id.Value);

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public IActionResult DecreaseCartItem(int? id)
        {
            if (id is null)
                return NotFound();

            CookieDecreaseInCart(id.Value);

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public IActionResult RemoveCartItem(int? id)
        {
            if (id is null)
                return NotFound();

            CookieRemoveFromCart(id.Value);

            return RedirectToAction(nameof(Cart));
        }

        private void CookieAddToCart(int articleId, int change = 1)
        {
            var options = new CookieOptions { Expires = DateTime.Now + TimeSpan.FromDays(7) };
            var key = $"{CartCookiePrefix}{articleId}";
            var count = Math.Max(
                (int.TryParse(Request.Cookies[key], out var v) ? v : 0) + change,
                0
            );

            if (count > 0)
                Response.Cookies.Append(key, count.ToString(), options);
            else
                CookieRemoveFromCart(articleId);
        }

        private void CookieDecreaseInCart(int articleId) => CookieAddToCart(articleId, -1);

        private void CookieRemoveFromCart(int articleId) =>
            Response.Cookies.Delete($"{CartCookiePrefix}{articleId}");

        private IEnumerable<(Article Article, int Count)> CookieGetArticles()
        {
            foreach (var (key, value) in Request.Cookies)
            {
                if (!key.StartsWith(CartCookiePrefix))
                    continue;

                var articleId = key.Replace(CartCookiePrefix, string.Empty).ParseIntOrDefault();
                var count = value.ParseIntOrDefault();

                if (articleId is null || count is null)
                    continue;

                var article = _context.Articles.Find(articleId.Value);

                if (article is null)
                    CookieRemoveFromCart(articleId.Value);
                else
                    yield return (article, count.Value);
            }
        }
    }
}
