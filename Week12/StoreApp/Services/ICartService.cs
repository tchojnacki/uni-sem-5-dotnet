using StoreApp.Models;
using System.Collections.Generic;

namespace StoreApp.Services
{
    public interface ICartService
    {
        public void IncrementArticleCount(int articleId);

        public void DecrementArticleCount(int articleId);

        public void RemoveArticle(int articleId);

        public IEnumerable<(Article Article, int Count)> GetAllItems();
    }
}
