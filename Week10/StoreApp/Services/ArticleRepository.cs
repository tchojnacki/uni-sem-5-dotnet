using System.Collections.Generic;
using System.Linq;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.DTOs;
using StoreApp.Models;

namespace StoreApp.Services
{
    public interface IArticleRepository
        : IRepository<ArticleDto, CreateArticleDto, UpdateArticleDto> { }

    public class ArticleRepository : IArticleRepository
    {
        private readonly StoreDbContext _context;
        private readonly IPhotoService _photoService;

        public ArticleRepository(StoreDbContext context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        public IEnumerable<ArticleDto> All =>
            _context.Articles
                .Include(a => a.Category)
                .OrderBy(a => a.Id)
                .Adapt<IEnumerable<ArticleDto>>();

        public ActionResult<ArticleDto> this[int id] =>
            _context.Articles
                .Include(a => a.Category)
                .FirstOrDefault(a => a.Id == id)
                ?.Adapt<ArticleDto>()
                is { } dto
                ? new OkObjectResult(dto)
                : new NotFoundResult();

        public ActionResult<ArticleDto> Add(CreateArticleDto dto)
        {
            var article = dto.Adapt<Article>();
            if (_context.Categories.Find(article.CategoryId) is null)
                return new BadRequestResult();

            _context.Add(article);
            _context.SaveChanges();
            return new CreatedResult($"api/articles/{article.Id}", article.Adapt<ArticleDto>());
        }

        public ActionResult<ArticleDto> Update(UpdateArticleDto dto)
        {
            var article = _context.Articles.Find(dto.Id);
            if (article is null)
                return new NotFoundResult();

            article.Name = dto.Name;
            article.Price = dto.Price;
            article.CategoryId = dto.CategoryId;

            if (_context.Categories.Find(article.CategoryId) is null)
                return new BadRequestResult();

            _context.Update(article);
            _context.SaveChanges();
            return article.Adapt<ArticleDto>();
        }

        public ActionResult Delete(int id)
        {
            var article = _context.Articles.Find(id);

            if (article is null)
                return new NotFoundResult();

            if (article.PhotoGuid is { } photoGuid)
                _photoService.RemovePhoto(photoGuid);

            _context.Articles.Remove(article);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
