using List5;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApp.Controllers
{
    public class ToolController : Controller
    {
        public IActionResult Solve(double a, double b, double c)
        {
            ViewBag.ParamDescription = $"Użyte parametry: a = {a:g5}, b = {b:g5}, c = {c:g5}";

            var solutionCount = Exercise1.SolveQuadratic(a, b, c, out var x1, out var x2);

            ViewBag.ResultText = solutionCount switch
            {
                0 => "Brak rozwiązań (równanie sprzeczne)",
                1 => $"Jedno rozwiązanie: x = {x1:g5}",
                2 => $"Dwa rozwiązania: x1 = {x1:g5}, x2 = {x2:g5}",
                -1 => "Nieskończenie wiele rozwiązań (równanie tożsamościowe)",
                _ => throw new InvalidOperationException()
            };

            ViewBag.ResultClass = solutionCount switch
            {
                0 => "text-danger font-italic",
                1 => "text-info font-weight-bold",
                2 => "text-success font-weight-bold",
                -1 => "text-warning font-italic",
                _ => throw new InvalidOperationException()
            };

            return View();
        }
    }
}
