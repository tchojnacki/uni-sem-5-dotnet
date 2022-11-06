using System;

namespace Exercise2
{
    internal static class Program
    {
        private static int ReadNumberCount()
        {
            int n;

            Console.Write("Podaj liczbę n: ");
            var nStr = Console.ReadLine();

            while (!int.TryParse(nStr, out n) || n < 1)
            {
                Console.WriteLine("Podano błędną wartość! Wymagana liczba całkowita n >= 1.");
                Console.Write("Podaj liczbę n: ");
                nStr = Console.ReadLine();
            }

            return n;
        }

        private static void Main()
        {
            var n = ReadNumberCount();
        }
    }
}
