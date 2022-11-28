using List5;

namespace WebApp.Services
{
    internal class QuadraticSolverAdapter : IQuadraticSolver
    {
        public int SolveQuadratic(double a, double b, double c, out double x1, out double x2) =>
            Exercise1.SolveQuadratic(a, b, c, out x1, out x2);
    }
}
