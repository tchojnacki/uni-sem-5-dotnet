using StoreApp.Models;
using System.Collections.Generic;

namespace StoreApp.Services
{
    public interface ICartService
    {
        public void IncreaseInCart(int articleId);

        public void DecreaseInCart(int articleId);

        public void RemoveFromCart(int articleId);

        public IEnumerable<(Article Article, int Count)> GetArticlesInCart();
    }
}
