using System;
using System.Diagnostics;

namespace Exercise6
{
    internal class Program
    {
        private static void Main()
        {
            // No arguments - all counts are zero
            Debug.Assert(CountMyTypes() == (0, 0, 0, 0));

            var typeCount = CountMyTypes(
                "first parameter", // long string
                3, // other
                5.5, // positive real
                "test", // other
                null, // other
                -8, // even integer
                new { Anonymous = true }, // other
                false, // other
                4, // even integer
                Math.PI, // positive real
                "my long string", // long string
                0, // even integer
                (1, 2, 3), // other
                -0.1, // other
                "Hello world!", // long string
                string.Empty, // other
                "Testing the method", // long string
                -128, // even integer
                "Longest string passed yet" // long string
            );

            Debug.Assert(typeCount == (4, 2, 5, 8));

            Console.WriteLine($"Even integers: {typeCount.EvenIntegerCount}");
            Console.WriteLine($"Real positive numbers: {typeCount.PositiveRealNumberCount}");
            Console.WriteLine($"Strings of length at least 5: {typeCount.LongStringCount}");
            Console.WriteLine($"Other elements: {typeCount.OtherElementCount}");
        }

        private static (
            int EvenIntegerCount,
            int PositiveRealNumberCount,
            int LongStringCount,
            int OtherElementCount
        ) CountMyTypes(params dynamic[] elements)
        {
            var evenIntegerCount = 0;
            var positiveRealNumberCount = 0;
            var longStringCount = 0;
            var otherElementCount = 0;

            foreach (var element in elements)
            {
                switch (element)
                {
                    case int i when i % 2 == 0:
                        evenIntegerCount++;
                        break;
                    case double
                    and > 0:
                        positiveRealNumberCount++;
                        break;
                    case string { Length: >= 5 }:
                        longStringCount++;
                        break;
                    default:
                        otherElementCount++;
                        break;
                }
            }

            return (evenIntegerCount, positiveRealNumberCount, longStringCount, otherElementCount);
        }
    }
}
