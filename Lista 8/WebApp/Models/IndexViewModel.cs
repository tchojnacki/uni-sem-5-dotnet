using System.Collections.Generic;

namespace WebApp.Models
{
    public class IndexViewModel
    {
        public IReadOnlyList<(double a, double b, double c)> QuadraticExamples { get; init; }
    }
}
