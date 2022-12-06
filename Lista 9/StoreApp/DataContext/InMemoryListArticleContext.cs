using System.Collections.Generic;
using System.Linq;
using StoreApp.Models;

namespace StoreApp.DataContext
{
    public class InMemoryListArticleContext : IArticleContext
    {
        private readonly List<Article> _articles = new();

        public IEnumerable<Article> GetAllArticles() => _articles;

        public Article? GetArticle(int id) => _articles.SingleOrDefault(a => a.Id == id);

        public void AddArticle(Article article)
        {
            var id = _articles.Any() ? _articles.Max(a => a.Id) + 1 : 0;
            article.Id = id;
            _articles.Add(article);
        }

        public void DeleteArticle(int id)
        {
            var article = _articles.SingleOrDefault(a => a.Id == id);
            if (article is not null)
                _articles.Remove(article);
        }

        public void UpdateArticle(Article article)
        {
            var articleToUpdate = _articles.SingleOrDefault(a => a.Id == article.Id);
            if (articleToUpdate is null)
                return;
            var index = _articles.IndexOf(articleToUpdate);
            _articles[index] = article;
        }
    }
}
