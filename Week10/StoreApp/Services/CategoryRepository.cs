using System.Collections.Generic;
using System.Linq;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Data;
using StoreApp.DTOs;
using StoreApp.Models;

namespace StoreApp.Services
{
    public interface ICategoryRepository
        : IRepository<CategoryDto, CreateCategoryDto, UpdateCategoryDto> { }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDbContext _context;

        public CategoryRepository(StoreDbContext context) => _context = context;

        public IEnumerable<CategoryDto> All =>
            _context.Categories.OrderBy(c => c.Id).Adapt<IEnumerable<CategoryDto>>();

        public ActionResult<CategoryDto> this[int id] =>
            _context.Categories.Find(id)?.Adapt<CategoryDto>() is { } dto
                ? new OkObjectResult(dto)
                : new NotFoundResult();

        public ActionResult<CategoryDto> Add(CreateCategoryDto dto)
        {
            var category = dto.Adapt<Category>();

            if (_context.Categories.Any(c => c.Name == category.Name))
                return new BadRequestResult();

            _context.Add(category);
            _context.SaveChanges();
            return new CreatedResult(
                $"api/categories/{category.Id}",
                category.Adapt<CategoryDto>()
            );
        }

        public ActionResult<CategoryDto> Update(UpdateCategoryDto dto)
        {
            var category = _context.Categories.Find(dto.Id);
            if (category is null)
                return new NotFoundResult();

            category.Name = dto.Name;

            if (_context.Categories.Any(c => c.Name == category.Name))
                return new BadRequestResult();

            _context.Update(category);
            _context.SaveChanges();
            return category.Adapt<CategoryDto>();
        }

        public ActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);

            if (category is null)
                return new NotFoundResult();

            if (_context.Articles.Any(a => a.CategoryId == id))
                return new BadRequestResult();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
