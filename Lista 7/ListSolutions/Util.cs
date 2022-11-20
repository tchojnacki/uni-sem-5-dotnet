using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Exercises
{
    internal static class Util
    {
        public static void PrintNestedCollection(IEnumerable collection, string indent = "")
        {
            var nextIndent = indent + "\t";
            Console.WriteLine(indent + "[");
            foreach (var element in collection)
            {
                if (element is IEnumerable nested)
                    PrintNestedCollection(nested, nextIndent);
                else
                    Console.WriteLine(nextIndent + element);
            }
            Console.WriteLine(indent + "],");
        }

        public static bool NestedEqualityCheck(IEnumerable<object> c1, IEnumerable<object> c2)
        {
            var l1 = c1.ToList();
            var l2 = c2.ToList();

            if (l1.Count != l2.Count)
                return false;

            foreach (var pair in l1.Zip(l2))
            {
                var elementsEqual = pair switch
                {
                    (IEnumerable<object> n1, IEnumerable<object> n2) => NestedEqualityCheck(n1, n2),
                    var (e1, e2) => e1.Equals(e2)
                };

                if (!elementsEqual)
                    return false;
            }

            return true;
        }
    }
}
