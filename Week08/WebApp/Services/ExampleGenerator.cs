using System;
using System.Collections.Generic;

namespace WebApp.Services
{
    internal class ExampleGenerator : IExampleGenerator
    {
        private static readonly Random Rand = new();

        private static double GenQuadraticCoeff()
        {
            var nominator = Rand.Next(-10, 11);
            var denominator = Rand.Next(1, 11);
            return (double)nominator / denominator;
        }

        public IEnumerable<(double a, double b, double c)> GenQuadraticExamples()
        {
            while (true)
            {
                yield return (GenQuadraticCoeff(), GenQuadraticCoeff(), GenQuadraticCoeff());
            }
            // ReSharper disable once IteratorNeverReturns
        }

        public IEnumerable<int> GenRangeExamples()
        {
            while (true)
            {
                yield return Rand.Next(1, 100);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}
