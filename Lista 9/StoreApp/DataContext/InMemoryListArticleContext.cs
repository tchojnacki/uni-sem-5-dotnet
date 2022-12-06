using System.Collections.Generic;
using System.Linq;
using StoreApp.Models;

namespace StoreApp.DataContext
{
    // TODO: make sure no methods throw
    public class InMemoryListArticleContext : IArticleContext
    {
        private int _nextId;
        private readonly List<Article> _articles = new();

        public IEnumerable<Article> GetAllArticles() => _articles;

        public Article? GetArticle(int id) => _articles.FirstOrDefault(a => a.Id == id);

        public void AddArticle(Article article)
        {
            article.Id = _nextId++;
            _articles.Add(article);
        }

        public void DeleteArticle(int id)
        {
            var article = _articles.FirstOrDefault(a => a.Id == id);
            if (article is not null)
                _articles.Remove(article);
        }

        public void UpdateArticle(Article article)
        {
            var articleToUpdate = _articles.FirstOrDefault(a => a.Id == article.Id);
            if (articleToUpdate is null)
                return;
            _articles[_articles.IndexOf(articleToUpdate)] = article;
        }
    }
}
