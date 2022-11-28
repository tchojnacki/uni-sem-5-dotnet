using System.Collections.Generic;

namespace WebApp.Services
{
    public interface IRandomQuadraticParams
    {
        IEnumerable<(double a, double b, double c)> GenerateParams();
    }
}
