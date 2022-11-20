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
            var studentsWithTopics = Generator.GenerateStudentsWithTopicsEasy();

            var resultFluent = Ex1.ChunkStudentsByNameFluent(studentsWithTopics, n).ToList();
            var resultQuery = Ex1.ChunkStudentsByNameQuery(studentsWithTopics, n).ToList();
            var resultExt = Ex1.ChunkStudentsByNameExt(studentsWithTopics, n).ToList();

            Util.PrintNestedCollection(resultFluent);
            Debug.Assert(Util.NestedEqualityCheck(resultFluent, resultQuery));
            Debug.Assert(Util.NestedEqualityCheck(resultFluent, resultExt));
        }

        private static void Main()
        {
            CheckEx1();
        }
    }
}
