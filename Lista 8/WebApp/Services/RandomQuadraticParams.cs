using System;
using System.Collections.Generic;

namespace WebApp.Services
{
    internal class RandomQuadraticParams : IRandomQuadraticParams
    {
        private static readonly Random Rand = new();

        private static double GenCoeff()
        {
            var nominator = Rand.Next(-10, 11);
            var denominator = Rand.Next(1, 11);
            return (double)nominator / denominator;
        }

        public IEnumerable<(double a, double b, double c)> GenerateParams()
        {
            while (true)
            {
                yield return (GenCoeff(), GenCoeff(), GenCoeff());
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}
