using StoreApp.Models;
using System.Collections.Generic;
using System;

namespace StoreApp.DataContext
{
    public class InMemoryDictionaryArticleContext : IArticleContext
    {
        private int _nextId;
        private readonly Dictionary<int, Article> _articles = new();

        public IEnumerable<Article> GetAllArticles() => _articles.Values;

        public Article? GetArticle(int id) => _articles.GetValueOrDefault(id);

        public void AddArticle(Article article)
        {
            article.Id = _nextId++;
            _articles[article.Id] = article;
        }

        public void DeleteArticle(int id)
        {
            if (!_articles.ContainsKey(id))
                throw new ArgumentException(nameof(id));

            _articles.Remove(id);
        }

        public void UpdateArticle(Article article)
        {
            if (!_articles.ContainsKey(article.Id))
                throw new ArgumentException(nameof(article));

            _articles[article.Id] = article;
        }
    }
}
