// Można uprościć, ale korzystając z technik nie pokazanych na wykładzie:
// - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/file-scoped-namespaces
// - https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements

using System;

namespace Exercise1
{
    internal static class Program
    {
        /// <summary>
        /// Rozwiązuje równanie kwadratowe ax^2 + bx + c = 0, gdzie <paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/> - dowolne liczby rzeczywiste.
        /// <example>
        /// <code>
        /// var solutionCount = SolveQuadratic(1, -4, 4, out var x1, out var x2);
        /// // solutionCount: 1, x1: 2, x2: NaN
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="a">współczynnik kwadratowy</param>
        /// <param name="b">współczynnik liniowy</param>
        /// <param name="c">współczynnik stały (wyraz wolny)</param>
        /// <param name="x1">pierwsze rozwiązanie równania, <c>NaN</c> jeżeli wartość zwrócona <c>&lt; 1</c></param>
        /// <param name="x2">drugie rozwiązanie równania, <c>NaN</c> jeżeli wartośc zwrócona <c>&lt; 2</c></param>
        /// <returns>
        /// <list type="bullet">
        /// <item>
        /// <term>-1</term>
        /// <description>jeżeli istnieje nieskończona liczba rozwiązań (równanie tożsamościowe)</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>jeżeli nie ma żadnych rozwiązań (równanie sprzeczne)</description>
        /// </item>
        /// <item>
        /// <term>1 (oraz ustawione <paramref name="x1"/>)</term>
        /// <description>jeżeli istnieje dokładnie jedno rozwiązanie</description>
        /// </item>
        /// <item>
        /// <term>2 (oraz ustawione <paramref name="x1"/>, <paramref name="x2"/>)</term>
        /// <description>jeżeli istnieją dokładnie dwa rozwiązania</description>
        /// </item>
        /// </list>
        /// </returns>
        private static int SolveQuadratic(
            double a,
            double b,
            double c,
            out double x1,
            out double x2
        )
        {
            x1 = double.NaN;
            x2 = double.NaN;

            if (a == 0) // bx + c = 0
            {
                if (b == 0) // c = 0
                    return c == 0 ? -1 : 0;

                x1 = -c / b;
                return 1;
            }

            // ax^2 + bx + c = 0, a =/= 0
            var delta = b * b - 4 * a * c;
            switch (delta)
            {
                case > 0:
                    var sqrtDelta = Math.Sqrt(delta);
                    x1 = (-b - sqrtDelta) / (2 * a);
                    x2 = (-b + sqrtDelta) / (2 * a);
                    return 2;
                case 0:
                    x1 = -b / (2 * a);
                    return 1;
                default:
                    return 0;
            }
        }

        private static double ReadNumber(string prompt)
        {
            string? input;
            double parsed;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
            } while (!double.TryParse(input, out parsed));

            return parsed;
        }

        private static void Main()
        {
            Console.WriteLine("Kalkulator rozwiązań równania kwadratowego ax^2 + bx + c = 0.");
            var a = ReadNumber("Podaj współczynnik kwadratowy a: ");
            var b = ReadNumber("Podaj współczynnik liniowy b: ");
            var c = ReadNumber("Podaj współczynnik stały c: ");

            var solutionCount = SolveQuadratic(a, b, c, out var x1, out var x2);

            switch (solutionCount)
            {
                case 0:
                    Console.WriteLine("Brak rozwiązań (równanie sprzeczne).");
                    break;
                case 1:
                    // Formatowanie złożone
                    Console.WriteLine("Jedno rozwiązanie: x = {0:g5}.", x1);
                    break;
                case 2:
                    // Interpolacja łańcucha znaków
                    Console.WriteLine($"Dwa rozwiązania: x1 = {x1:g5}, x2 = {x2:g5}.");
                    break;
                case -1:
                    Console.WriteLine("Nieskończenie wiele rozwiązań (równanie tożsamościowe).");
                    break;
            }
        }
    }
}
