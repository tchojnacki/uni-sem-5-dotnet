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

            Util.PrintEnumerable(resultMbs);
            Debug.Assert(Util.StructuralEquality(resultMbs, resultQes));
            Debug.Assert(Util.StructuralEquality(resultMbs, resultExt));
        }

        private static void CheckEx2()
        {
            Console.WriteLine("Exercise 2)");

            var students = Generator.GenerateStudentsWithTopicsEasy();

            Console.WriteLine("a)");

            var resultMbsA = Ex2.TopicsByPopularityMbs(students).ToList();
            var resultQesA = Ex2.TopicsByPopularityQes(students).ToList();

            Util.PrintEnumerable(resultMbsA);
            Debug.Assert(Util.StructuralEquality(resultMbsA, resultQesA));

            Console.WriteLine("b)");

            var resultMbsB = Ex2.TopicsByPopularityByGenderMbs(students).ToList();
            var resultQesB = Ex2.TopicsByPopularityByGenderQes(students).ToList();

            Util.PrintGroupedEnumerable(resultMbsB);
            Debug.Assert(Util.GroupedStructuralEquality(resultMbsB, resultQesB));
        }

        private static void CheckEx3()
        {
            Console.WriteLine("Exercise 3)");

            var students = Generator.GenerateStudentsWithTopicsEasy();

            Console.WriteLine("a)");

            var resultMbsA = Ex3.ConvertStudentsMbs(students, Ex3.HardcodedTopics);
            var resultsQesA = Ex3.ConvertStudentsQes(students, Ex3.HardcodedTopics);

            Console.WriteLine("Before:");
            Util.PrintEnumerable(students);
            Console.WriteLine("After:");
            Util.PrintEnumerable(resultMbsA);
            Debug.Assert(Util.StructuralEquality(resultMbsA, resultsQesA));

            Console.WriteLine("b)");

            var resultMbsB = Ex3.ConvertStudentsMbs(students, Ex3.ExtractTopicsMbs(students));
            var resultQesB = Ex3.ConvertStudentsQes(students, Ex3.ExtractTopicsQes(students));

            Console.WriteLine("Before:");
            Util.PrintEnumerable(students);
            Console.WriteLine("After:");
            Util.PrintEnumerable(resultMbsB);
            Debug.Assert(Util.StructuralEquality(resultMbsB, resultQesB));
        }

        private static void Main()
        {
            CheckEx1();
            CheckEx2();
            CheckEx3();
        }
    }
}
