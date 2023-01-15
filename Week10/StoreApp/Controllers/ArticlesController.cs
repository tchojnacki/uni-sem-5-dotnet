using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;
using StoreApp.Services;
using StoreApp.Util;
using StoreApp.ViewModels;

namespace StoreApp.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class ArticlesController : Controller
    {
        private readonly StoreDbContext _context;
        private readonly IPhotoService _photoService;

        public ArticlesController(StoreDbContext context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        public IActionResult Index() => View(_context.Articles.Include(a => a.Category).GetPage());

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article is null)
                return NotFound();

            return View(article);
        }

        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [Bind("Name,Price,CategoryId,Photo")] ArticleCreateViewModel articleView
        )
        {
            if (ModelState.IsValid)
            {
                Guid? photoGuid = null;
                if (articleView.Photo is { } file)
                    photoGuid = await _photoService.AddPhotoAsync(file);

                var article = new Article
                {
                    Name = articleView.Name,
                    Price = articleView.Price,
                    CategoryId = articleView.CategoryId,
                    PhotoGuid = photoGuid
                };

                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Categories"] = new SelectList(
                _context.Categories,
                "Id",
                "Name",
                articleView.CategoryId
            );

            return View(articleView);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            var article = await _context.Articles.FindAsync(id);

            if (article is null)
                return NotFound();

            ViewData["Categories"] = new SelectList(
                _context.Categories,
                "Id",
                "Name",
                article.CategoryId
            );

            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Name,Price,CategoryId,PhotoGuid")] Article article
        )
        {
            if (id != article.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Categories"] = new SelectList(
                _context.Categories,
                "Id",
                "Name",
                article.CategoryId
            );

            return View(article);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article is null)
                return NotFound();

            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article is null)
                return NotFound();

            if (article.PhotoGuid is { } photoGuid)
                _photoService.RemovePhoto(photoGuid);

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id) => _context.Articles.Any(e => e.Id == id);
    }
}
