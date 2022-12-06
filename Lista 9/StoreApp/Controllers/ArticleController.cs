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

        public ActionResult Details(int id) => View(_articleContext.GetArticle(id));

        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            try
            {
                if (ModelState.IsValid)
                    _articleContext.AddArticle(article);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id) => View(_articleContext.GetArticle(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Article article)
        {
            try
            {
                if (ModelState.IsValid)
                    _articleContext.UpdateArticle(article);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id) => View(_articleContext.GetArticle(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Article _)
        {
            try
            {
                _articleContext.DeleteArticle(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
