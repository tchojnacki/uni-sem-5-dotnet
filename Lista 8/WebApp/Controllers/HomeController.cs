using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRandomQuadraticParams _randomQuadraticParams;

        public HomeController(IRandomQuadraticParams randomQuadraticParams) =>
            _randomQuadraticParams = randomQuadraticParams;

        public IActionResult Index()
        {
            return View(
                new IndexViewModel
                {
                    QuadraticExamples = _randomQuadraticParams.GenerateParams().Take(10).ToList()
                }
            );
        }

        public IActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}
