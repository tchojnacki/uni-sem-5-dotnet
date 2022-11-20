using System;
using System.Diagnostics;
using System.Linq;
using LectureExamples;

namespace Exercises
{
    internal class Program
    {
        private static void CheckEx1()
        {
            Console.WriteLine("Exercise 1)");

            const int n = 5;
            var students = Generator.GenerateStudentsWithTopicsEasy();

            var resultMbs = Ex1.ChunkStudentsByNameMbs(students, n).ToList();
            var resultQes = Ex1.ChunkStudentsByNameQes(students, n).ToList();
            var resultExt = Ex1.ChunkStudentsByNameExt(students, n).ToList();

            Util.PrintNestedCollection(resultMbs);
            Debug.Assert(Util.NestedEqualityCheck(resultMbs, resultQes));
            Debug.Assert(Util.NestedEqualityCheck(resultMbs, resultExt));
        }

        private static void CheckEx2()
        {
            Console.WriteLine("Exercise 2)");

            var students = Generator.GenerateStudentsWithTopicsEasy();

            Console.WriteLine("a)");

            var resultMbsA = Ex2.TopicsByPopularityMbs(students).ToList();
            var resultQesA = Ex2.TopicsByPopularityQes(students).ToList();

            Util.PrintNestedCollection(resultMbsA);
            Debug.Assert(Util.NestedEqualityCheck(resultMbsA, resultQesA));

            Console.WriteLine("b)");

            var resultMbsB = Ex2.TopicsByPopularityByGenderMbs(students).ToList();
            var resultQesB = Ex2.TopicsByPopularityByGenderQes(students).ToList();

            Util.PrintGroupedCollections(resultMbsB);
            Debug.Assert(Util.GroupedEqualityCheck(resultMbsB, resultQesB));
        }

        private static void Main()
        {
            CheckEx1();
            CheckEx2();
        }
    }
}
