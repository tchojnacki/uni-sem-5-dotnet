using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using StoreApp.Models;

namespace StoreApp.DataContext
{
    public class InMemoryCollectionArticleContext : IArticleContext
    {
        private int _nextId;
        private readonly SynchronizedCollection<Article> _articles = new();

        public IEnumerable<Article> GetAllArticles() => _articles;

        public Article? GetArticle(int id) => _articles.FirstOrDefault(a => a.Id == id);

        public void AddArticle(Article article)
        {
            article.Id = Interlocked.Increment(ref _nextId);
            _articles.Add(article);
        }

        public void DeleteArticle(int id)
        {
            var article = _articles.FirstOrDefault(a => a.Id == id);

            if (article is null)
                throw new ArgumentException(nameof(id));

            _articles.Remove(article);
        }

        public void UpdateArticle(Article article)
        {
            var articleToUpdate = _articles.FirstOrDefault(a => a.Id == article.Id);

            if (articleToUpdate is null)
                throw new ArgumentException(nameof(article));

            _articles[_articles.IndexOf(articleToUpdate)] = article;
        }
    }
}
