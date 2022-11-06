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
            int? currentLargest = null;
            int? currentSecondLargest = null;

            var n = ReadNumberCount();
            Console.WriteLine($"Podaj {n} liczb:");

            while (n > 0)
            {
                var line = Console.ReadLine() ?? "";
                foreach (var word in line.Split())
                {
                    if (!int.TryParse(word, out var number))
                        continue;

                    if (number > currentLargest || currentLargest == null)
                    {
                        currentSecondLargest = currentLargest;
                        currentLargest = number;
                    }
                    else if (
                        number != currentLargest
                        && (number > currentSecondLargest || currentSecondLargest == null)
                    )
                    {
                        currentSecondLargest = number;
                    }

                    n--;
                    if (n == 0)
                        break;
                }
            }

            if (currentSecondLargest == null)
            {
                Console.WriteLine("brak rozwiązania");
            }
            else
            {
                Console.WriteLine(currentSecondLargest);
            }
        }
    }
}
