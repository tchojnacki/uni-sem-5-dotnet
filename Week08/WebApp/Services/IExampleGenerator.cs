using System.Collections.Generic;

namespace WebApp.Services
{
    public interface IExampleGenerator
    {
        IEnumerable<(double a, double b, double c)> GenQuadraticExamples();
        IEnumerable<int> GenRangeExamples();
    }
}
