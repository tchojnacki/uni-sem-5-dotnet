using System.Collections.Generic;
using System.Linq;
using LectureExamples;

namespace Exercises
{
    internal static class Ex2
    {
        public static IEnumerable<string> TopicsByPopularityMbs(
            IEnumerable<StudentWithTopics> students
        )
        {
            return students
                .SelectMany(s => s.Topics)
                .GroupBy(t => t)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key);
        }

        public static IEnumerable<string> TopicsByPopularityQes(
            IEnumerable<StudentWithTopics> students
        )
        {
            return from s in students
            from t in s.Topics
            group t by t into g
            orderby g.Count() descending
            select g.Key;
        }

        public static IEnumerable<(
            Gender Gender,
            IEnumerable<string> TopicsByPopularity
        )> TopicsByPopularityByGenderMbs(IEnumerable<StudentWithTopics> students)
        {
            return students.GroupBy(
                keySelector: s => s.Gender,
                resultSelector: (key, sl) => (key, TopicsByPopularityMbs(sl))
            );
        }

        public static IEnumerable<(
            Gender Gender,
            IEnumerable<string> TopicsByPopularity
        )> TopicsByPopularityByGenderQes(IEnumerable<StudentWithTopics> students)
        {
            return from s in students
            group s by s.Gender into g
            select (g.Key, TopicsByPopularityQes(g));
        }
    }
}
