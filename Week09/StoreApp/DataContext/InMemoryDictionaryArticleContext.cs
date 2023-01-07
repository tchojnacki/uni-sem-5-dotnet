using StoreApp.Models;
using System.Collections.Generic;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace StoreApp.DataContext
{
    public class InMemoryDictionaryArticleContext : IArticleContext
    {
        private int _nextId;
        private readonly ConcurrentDictionary<int, Article> _articles = new();

        public IEnumerable<Article> GetAllArticles() => _articles.Values;

        public Article? GetArticle(int id) => _articles.GetValueOrDefault(id);

        public void AddArticle(Article article)
        {
            article.Id = Interlocked.Increment(ref _nextId);
            _articles[article.Id] = article;
        }

        public void DeleteArticle(int id)
        {
            if (!_articles.ContainsKey(id))
                throw new ArgumentException(nameof(id));

            _articles.Remove(id, out _);
        }

        public void UpdateArticle(Article article)
        {
            if (!_articles.ContainsKey(article.Id))
                throw new ArgumentException(nameof(article));

            _articles[article.Id] = article;
        }
    }
}
