// ReSharper disable ParameterTypeCanBeEnumerable.Local

using System;

namespace Exercise3
{
    internal class Program
    {
        private static readonly Random Rand = new(0);

        private static void Main()
        {
            Console.WriteLine("1) Array.Empty");
            var array = Array.Empty<int>();
            Console.Write("Empty array: ");
            PrintArray(array);

            Console.WriteLine("2) Array.Resize");
            Array.Resize(ref array, 10);
            Console.Write("Resized array: ");
            PrintArray(array);

            for (var i = 0; i < array.Length; i++)
                array[i] = Rand.Next(-50, 50);
            Console.Write("Filled array: ");
            PrintArray(array);

            Console.WriteLine("3) Array.Sort");
            Array.Sort(array, (a, b) => b - a);
            Console.Write("Sorted array: ");
            PrintArray(array);

            Console.WriteLine("4) Array.Resize");
            Array.Resize(ref array, 7);
            Console.Write("Resized array: ");
            PrintArray(array);

            Console.WriteLine("5) Array.Reverse");
            Array.Reverse(array);
            Console.Write("Reversed array: ");
            PrintArray(array);

            Console.WriteLine("6) Array.BinarySearch");
            const int searched = 31;
            var index = Array.BinarySearch(array, searched);
            Console.WriteLine($"Index of {searched} is {index}");
        }

        private static void PrintArray<T>(T[] array)
        {
            Console.WriteLine("[{0}]", string.Join(", ", array));
        }
    }
}
