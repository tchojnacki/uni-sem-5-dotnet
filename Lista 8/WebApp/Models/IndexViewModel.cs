using System.Collections.Generic;

namespace WebApp.Models
{
    public class IndexViewModel
    {
        public IReadOnlyList<(double a, double b, double c)> QuadraticExamples { get; init; }
        public IReadOnlyList<int> SetExamples { get; init; }
        public IReadOnlyList<int> GuessExamples { get; init; }
    }
}
