using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public IActionResult InvalidSolve()
        {
            Response.StatusCode = 400;
            return View();
        }

        public IActionResult InvalidSet()
        {
            Response.StatusCode = 400;
            return View();
        }

        public IActionResult InvalidGuess()
        {
            Response.StatusCode = 400;
            return View();
        }
    }
}
