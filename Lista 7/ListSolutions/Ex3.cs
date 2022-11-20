using System.Collections.Generic;
using System.Linq;
using LectureExamples;

namespace Exercises
{
    internal static class Ex3
    {
        public static readonly IReadOnlyList<Topic> HardcodedTopics = new List<Topic>
        {
            new(1, "C#"),
            new(2, "algorithms"),
            new(3, "Java"),
            new(4, "PHP"),
            new(5, "C++"),
            new(6, "fuzzy logic"),
            new(7, "Basic"),
            new(8, "JavaScript"),
            new(9, "neural networks"),
            new(10, "web programming")
        };

        public static List<Student> ConvertStudentsMbs(
            List<StudentWithTopics> oldStudents,
            IReadOnlyList<Topic> topicSet
        )
        {
            return oldStudents
                .Select(
                    s =>
                        new Student(
                            s.Id,
                            s.Index,
                            s.Name,
                            s.Gender,
                            s.Active,
                            s.DepartmentId,
                            s.Topics.Select(tn => topicSet.Single(t => t.Name == tn).Id).ToList()
                        )
                )
                .ToList();
        }

        public static List<Student> ConvertStudentsQes(
            List<StudentWithTopics> oldStudents,
            IReadOnlyList<Topic> topicSet
        )
        {
            return (
                from s in oldStudents
                select new Student(
                    s.Id,
                    s.Index,
                    s.Name,
                    s.Gender,
                    s.Active,
                    s.DepartmentId,
                    (from tn in s.Topics select topicSet.Single(t => t.Name == tn).Id).ToList()
                )
            ).ToList();
        }

        public static List<Topic> ExtractTopicsMbs(IEnumerable<StudentWithTopics> students)
        {
            return students
                .SelectMany(s => s.Topics)
                .Distinct()
                .Select((t, i) => new Topic(i + 1, t))
                .ToList();
        }

        public static List<Topic> ExtractTopicsQes(IEnumerable<StudentWithTopics> students)
        {
            var id = 1;
            return (
                from t in (from s in students from t in s.Topics select t).Distinct()
                select new Topic(id++, t)
            ).ToList();
        }

        public static void ConvertDatabase(
            IEnumerable<StudentWithTopics> oldStudents,
            out List<Ex3c.Student> newStudentList,
            out List<Topic> newTopicList,
            out List<Ex3c.StudentToTopic> newStudentToTopicList
        )
        {
            var oldStudentList = oldStudents.ToList();
            var topicList = ExtractTopicsMbs(oldStudentList);

            newTopicList = topicList;
            newStudentList = oldStudentList
                .Select(
                    s => new Ex3c.Student(s.Id, s.Index, s.Name, s.Gender, s.Active, s.DepartmentId)
                )
                .ToList();
            newStudentToTopicList = oldStudentList
                .SelectMany(
                    s =>
                        s.Topics.Select(
                            tn =>
                                new Ex3c.StudentToTopic(
                                    s.Id,
                                    topicList.Single(t => t.Name == tn).Id
                                )
                        )
                )
                .ToList();
        }
    }
}
