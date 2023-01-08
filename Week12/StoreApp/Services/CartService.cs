using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using StoreApp.Data;
using StoreApp.Models;
using StoreApp.Util;

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
                RemoveFromCart(articleId);
        }

        public void IncreaseInCart(int articleId) => ChangeAmountInCart(articleId, 1);

        public void DecreaseInCart(int articleId) => ChangeAmountInCart(articleId, -1);

        public void RemoveFromCart(int articleId) =>
            HttpContext?.Response.Cookies.Delete($"{CartCookiePrefix}{articleId}");

        public IEnumerable<(Article Article, int Count)> GetArticlesInCart()
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
                    RemoveFromCart(articleId.Value);
                else
                    yield return (article, count.Value);
            }
        }
    }
}
