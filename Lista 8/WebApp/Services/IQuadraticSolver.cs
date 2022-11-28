namespace WebApp.Services
{
    public interface IQuadraticSolver
    {
        int SolveQuadratic(double a, double b, double c, out double x1, out double x2);
    }
}
