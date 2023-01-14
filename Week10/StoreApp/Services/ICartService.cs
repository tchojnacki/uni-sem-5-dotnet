using StoreApp.ViewModels;

namespace StoreApp.Services
{
    public interface ICartService
    {
        public void IncrementArticleCount(int articleId);

        public void DecrementArticleCount(int articleId);

        public void RemoveArticle(int articleId);

        public void ClearAll();

        public CartViewModel GetCart();
    }
}
