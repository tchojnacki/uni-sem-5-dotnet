using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using StoreApp.Data;
using StoreApp.Util;
using StoreApp.ViewModels;

namespace StoreApp.Services
{
    public class CartService : ICartService
    {
        private const string CartCookiePrefix = "article";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly StoreDbContext _dbContext;

        public CartService(IHttpContextAccessor httpContextAccessor, StoreDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        private HttpContext? HttpContext => _httpContextAccessor.HttpContext;

        private void ChangeAmountInCart(int articleId, int change)
        {
            var options = new CookieOptions { Expires = DateTime.Now + TimeSpan.FromDays(7) };
            var key = $"{CartCookiePrefix}{articleId}";
            var oldCount = int.TryParse(HttpContext?.Request.Cookies[key], out var v) ? v : 0;
            var count = Math.Max(oldCount + change, 0);

            if (count > 0)
                HttpContext?.Response.Cookies.Append(key, count.ToString(), options);
            else
                RemoveArticle(articleId);
        }

        public void IncrementArticleCount(int articleId) => ChangeAmountInCart(articleId, 1);

        public void DecrementArticleCount(int articleId) => ChangeAmountInCart(articleId, -1);

        public void RemoveArticle(int articleId) =>
            HttpContext?.Response.Cookies.Delete($"{CartCookiePrefix}{articleId}");

        public void ClearAll()
        {
            foreach (var item in GetCartItems())
                RemoveArticle(item.Article.Id);
        }

        public CartViewModel GetCart() => new() { Items = GetCartItems() };

        private IEnumerable<CartViewModel.Item> GetCartItems()
        {
            if (HttpContext is null)
                yield break;

            foreach (var (key, value) in HttpContext!.Request.Cookies)
            {
                if (!key.StartsWith(CartCookiePrefix))
                    continue;

                var articleId = key.Replace(CartCookiePrefix, string.Empty).ParseIntOrDefault();
                var count = value.ParseIntOrDefault();

                if (articleId is null || count is null)
                    continue;

                var article = _dbContext.Articles.Find(articleId.Value);

                if (article is null)
                    RemoveArticle(articleId.Value);
                else
                    yield return new CartViewModel.Item { Article = article, Count = count.Value };
            }
        }
    }
}
