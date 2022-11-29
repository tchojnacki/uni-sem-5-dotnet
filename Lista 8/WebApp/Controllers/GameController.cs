using System;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class GameController : Controller
    {
        #region Logic

        public const int DefaultN = 30;
        private static readonly Random Rand = new();

        private static int _n = DefaultN;
        private static int _randValue = Rand.Next(_n);
        private static int _guessAttempts;

        private static void RerollNumber()
        {
            _randValue = Rand.Next(_n);
            _guessAttempts = 0;
        }

        private static void UpdateRange(int n)
        {
            _n = n;
            RerollNumber();
        }

        #endregion

        #region Actions

        public IActionResult Set(int n)
        {
            UpdateRange(n);

            ViewBag.NewRange = n;

            return View();
        }

        public IActionResult Draw()
        {
            RerollNumber();

            ViewBag.Range = _n;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Guess(int guess)
        {
            _guessAttempts++;
            ViewBag.GuessAttempts = _guessAttempts;

            ViewBag.ResultText = guess switch
            {
                _ when guess < 0 || guess >= _n => "Podano liczbę spoza zakresu.",
                _ when guess < _randValue => "Podano za małą liczbę.",
                _ when guess > _randValue => "Podano za dużą liczbę.",
                _ => "Poprawna odpowiedź!"
            };

            ViewBag.ResultClass = guess == _randValue ? "text-success" : "text-danger";

            if (guess == _randValue)
                RerollNumber();

            return View();
        }

        #endregion
    }
}
