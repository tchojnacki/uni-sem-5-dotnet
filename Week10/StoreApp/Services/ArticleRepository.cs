using System.Collections.Generic;
using System.Linq;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Services
{
    public class ArticleRepository : IRepository<Article>
    {
        private readonly StoreDbContext _context;
        private readonly IPhotoService _photoService;

        public ArticleRepository(StoreDbContext context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        public IEnumerable<Article> All => _context.Articles.OrderBy(a => a.Id);

        public Article? this[int id] => _context.Articles.Find(id);

        public Article Add(Article article)
        {
            article.Id = default;
            article.PhotoGuid = default;
            _context.Add(article);
            _context.SaveChanges();
            return article;
        }

        public Article Update(Article article)
        {
            _context.Update(article);
            _context.SaveChanges();
            return article;
        }

        public void Delete(int id)
        {
            var article = _context.Articles.Find(id);

            if (article is null)
                return;

            if (article.PhotoGuid is { } photoGuid)
                _photoService.RemovePhoto(photoGuid);

            _context.Articles.Remove(article);
            _context.SaveChanges();
        }
    }
}
