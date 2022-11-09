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

        private static int ReadNextNumber(ref string buffer)
        {
            while (true)
            {
                while (string.IsNullOrWhiteSpace(buffer))
                    buffer = Console.ReadLine() ?? "";

                buffer = buffer.Trim();

                var readUntilIndex = buffer.IndexOfAny(new[] { ' ', '\t', '\n' });
                if (readUntilIndex == -1)
                    readUntilIndex = buffer.Length;

                var token = buffer[..readUntilIndex];
                buffer = buffer[readUntilIndex..];

                if (int.TryParse(token, out var output))
                    return output;
            }
        }

        private static void Main()
        {
            int? largest = null;
            int? secondLargest = null;

            var n = ReadNumberCount();
            Console.WriteLine($"Podaj {n} liczb:");

            var buffer = "";
            for (var i = 0; i < n; i++)
            {
                var number = ReadNextNumber(ref buffer);

                if (number == largest || number == secondLargest)
                    continue;

                if (number > largest || largest == null)
                {
                    secondLargest = largest;
                    largest = number;
                }
                else if (number > secondLargest || secondLargest == null)
                {
                    secondLargest = number;
                }
            }

            Console.WriteLine(secondLargest == null ? "brak rozwiązania" : secondLargest);
        }
    }
}
