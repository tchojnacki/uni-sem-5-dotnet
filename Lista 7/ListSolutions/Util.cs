using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Exercises
{
    internal static class Util
    {
        public static void PrintEnumerable(IEnumerable collection, string indent = "")
        {
            var nextIndent = indent + "\t";
            Console.WriteLine(indent + "[");
            foreach (var element in collection)
            {
                if (element is IEnumerable nested and not string)
                    PrintEnumerable(nested, nextIndent);
                else
                    Console.WriteLine(nextIndent + element + ",");
            }
            Console.WriteLine(indent + "],");
        }

        public static bool StructuralEquality<T>(IEnumerable<T> c1, IEnumerable<T> c2)
        {
            var l1 = c1.ToList();
            var l2 = c2.ToList();

            if (l1.Count != l2.Count)
                return false;

            foreach (var pair in l1.Zip(l2))
            {
                var elementsEqual = pair switch
                {
                    (IEnumerable<object> n1, IEnumerable<object> n2) => StructuralEquality(n1, n2),
                    var (e1, e2) => e1.Equals(e2)
                };

                if (!elementsEqual)
                    return false;
            }

            return true;
        }

        public static void PrintGroupedEnumerable<TK, TV>(
            IEnumerable<(TK Key, IEnumerable<TV> Items)> groups
        )
        {
            foreach (var group in groups)
            {
                Console.WriteLine("Group: " + group.Key);
                Console.WriteLine("Elements:");
                PrintEnumerable(group.Items);
            }
        }

        public static bool GroupedStructuralEquality<TK, TV>(
            IEnumerable<(TK Key, IEnumerable<TV> Items)> g1,
            IEnumerable<(TK Key, IEnumerable<TV> Items)> g2
        )
        {
            var l1 = g1.ToList();
            var l2 = g2.ToList();

            if (l1.Count != l2.Count)
                return false;

            return !l1.Zip(l2)
                .Any(
                    pair =>
                        !pair.First.Key.Equals(pair.Second.Key)
                        || !StructuralEquality(pair.First.Items, pair.Second.Items)
                );
        }
    }
}
