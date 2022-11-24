using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

            Console.WriteLine("c)");

            Ex3.ConvertDatabase(
                students,
                out var newStudentList,
                out var newTopicList,
                out var newStudentTopicList
            );

            Console.WriteLine("Student:");
            Util.PrintEnumerable(newStudentList);
            Console.WriteLine("Topic:");
            Util.PrintEnumerable(newTopicList);
            Console.WriteLine("StudentToTopic:");
            Util.PrintEnumerable(newStudentTopicList);
        }

        private static void CheckEx4()
        {
            Console.WriteLine("Exercise 4)");

            var studentType = typeof(Student);
            var topicType = Type.GetType("Exercises.Topic")!;

            // a)
            var student = Activator.CreateInstance(
                studentType,
                123,
                54321,
                "Smith",
                Gender.Male,
                true,
                1,
                new List<int> { 1, 4, 5 }
            )!;

            var topic1 = Assembly
                .GetExecutingAssembly()
                .CreateInstance(
                    "Exercises.Topic",
                    false,
                    BindingFlags.Default,
                    null,
                    new object[] { 1, "C#" },
                    null,
                    null
                );

            var topic2 = Activator.CreateInstance(topicType, 9, "neural networks");

            // b)
            var methodInfo = student
                .GetType()
                .GetMethod("IsInterestedInTopic", new[] { topicType })!;

            var result1 = methodInfo.Invoke(student, new[] { topic1 });
            var result2 = methodInfo.Invoke(student, new[] { topic2 });

            Console.WriteLine($"Is |{student}| interested in:");
            Console.WriteLine($"|{topic1}|? {result1}");
            Console.WriteLine($"|{topic2}|? {result2}");
        }

        private static void Main()
        {
            CheckEx1();
            CheckEx2();
            CheckEx3();
            CheckEx4();
        }
    }
}
