using System.Collections.Generic;
using StoreApp.Models;

namespace StoreApp.DataContext
{
    public interface IArticleContext
    {
        IEnumerable<Article> GetAllArticles();
        Article? GetArticle(int id);
        void AddArticle(Article article);
        void DeleteArticle(int id);
        void UpdateArticle(Article article);
    }
}
