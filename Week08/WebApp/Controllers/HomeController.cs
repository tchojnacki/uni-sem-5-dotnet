using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExampleGenerator _exampleGenerator;

        public HomeController(IExampleGenerator exampleGenerator) =>
            _exampleGenerator = exampleGenerator;

        public IActionResult Index()
        {
            return View(
                new IndexViewModel
                {
                    QuadraticExamples = _exampleGenerator.GenQuadraticExamples().Take(20).ToList(),
                    SetExamples = _exampleGenerator
                        .GenRangeExamples()
                        .Distinct()
                        .Take(5)
                        .OrderBy(n => n)
                        .ToList(),
                    GuessExamples = _exampleGenerator
                        .GenRangeExamples()
                        .Distinct()
                        .Take(10)
                        .OrderBy(n => n)
                        .ToList()
                }
            );
        }
    }
}
