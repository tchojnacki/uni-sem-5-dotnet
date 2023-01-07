using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class GameController : Controller
    {
        #region Logic

        public const int DefaultN = 30;
        private readonly Random _rand = new();

        private int N
        {
            get => HttpContext.Session.GetInt32(nameof(N)) ?? DefaultN;
            set => HttpContext.Session.SetInt32(nameof(N), value);
        }

        private int RandValue
        {
            get => HttpContext.Session.GetInt32(nameof(RandValue)) ?? _rand.Next(N);
            set => HttpContext.Session.SetInt32(nameof(RandValue), value);
        }

        private int GuessAttempts
        {
            get => HttpContext.Session.GetInt32(nameof(GuessAttempts)) ?? 0;
            set => HttpContext.Session.SetInt32(nameof(GuessAttempts), value);
        }

        private void RerollNumber()
        {
            RandValue = _rand.Next(N);
            GuessAttempts = 0;
        }

        private void UpdateRange(int n)
        {
            N = n;
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

            ViewBag.Range = N;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Guess(int guess)
        {
            ViewBag.GuessAttempts = ++GuessAttempts;

            ViewBag.ResultText = guess switch
            {
                _ when guess < 0 || guess >= N => "Podano liczbę spoza zakresu.",
                _ when guess < RandValue => "Podano za małą liczbę.",
                _ when guess > RandValue => "Podano za dużą liczbę.",
                _ => "Poprawna odpowiedź!"
            };

            ViewBag.ResultClass = guess == RandValue ? "text-success" : "text-danger";

            if (guess == RandValue)
                RerollNumber();

            return View();
        }

        #endregion
    }
}
