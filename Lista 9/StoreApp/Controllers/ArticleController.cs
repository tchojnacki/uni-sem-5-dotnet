using Microsoft.AspNetCore.Mvc;
using StoreApp.DataContext;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleContext _articleContext;

        public ArticleController(IArticleContext articleContext) =>
            _articleContext = articleContext;

        public ActionResult Index() => View(_articleContext.GetAllArticles());

        public ActionResult Details(int? id)
        {
            if (id is null)
                return new BadRequestResult();

            var article = _articleContext.GetArticle((int)id);

            if (article is null)
                return new NotFoundResult();

            return View(article);
        }

        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(Article article)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _articleContext.AddArticle(article);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error while adding the article!");
            }

            return View(article);
        }

        public ActionResult Edit(int? id)
        {
            if (id is null)
                return new BadRequestResult();

            var article = _articleContext.GetArticle((int)id);

            if (article is null)
                return new NotFoundResult();

            return View(article);
        }

        [HttpPost]
        public ActionResult Edit(int? id, Article article)
        {
            if (id is null)
                return new BadRequestResult();

            try
            {
                if (ModelState.IsValid)
                {
                    _articleContext.UpdateArticle(article);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error while updating the article!");
            }

            return View(article);
        }

        public ActionResult Delete(int? id)
        {
            if (id is null)
                return new BadRequestResult();

            var article = _articleContext.GetArticle((int)id);

            if (article is null)
                return new NotFoundResult();

            return View(article);
        }

        [HttpPost]
        public ActionResult Delete(int? id, Article article)
        {
            if (id is null)
                return new BadRequestResult();

            try
            {
                _articleContext.DeleteArticle((int)id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error while deleting the article!");
            }

            return View(article);
        }
    }
}
