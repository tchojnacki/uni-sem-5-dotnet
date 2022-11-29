using System;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class GameController : Controller
    {
        public const int DefaultN = 30;

        private static readonly Random Rand = new();

        private static int N = DefaultN;
        private static int RandValue = Rand.Next(N);
        private static int GuessAttempts = 0;

        public IActionResult Set(int n)
        {
            return View();
        }

        public IActionResult Draw()
        {
            return View();
        }

        public IActionResult Guess(int guess)
        {
            return View();
        }
    }
}
