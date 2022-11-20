using System.Collections.Generic;
using System.Linq;
using LectureExamples;

namespace Exercises
{
    internal static class Ex1
    {
        public static IEnumerable<StudentWithTopics[]> ChunkStudentsByNameMbs(
            IEnumerable<StudentWithTopics> students,
            int n
        )
        {
            return students
                .OrderBy(s => s.Name)
                .ThenBy(s => s.Index)
                .Select((s, i) => new { s, i })
                .GroupBy(si => si.i / n)
                .Select(g => g.Select(y => y.s).ToArray());
        }

        public static IEnumerable<StudentWithTopics[]> ChunkStudentsByNameQes(
            IEnumerable<StudentWithTopics> students,
            int n
        )
        {
            var i = 0;
            return from s in students
            orderby s.Name, s.Index
            group s by i++ / n into g
            select g.ToArray();
        }

        public static IEnumerable<StudentWithTopics[]> ChunkStudentsByNameExt(
            IEnumerable<StudentWithTopics> students,
            int n
        )
        {
            return students.OrderBy(s => s.Name).ThenBy(s => s.Index).Chunk(n);
        }

        private static IEnumerable<T[]> Chunk<T>(this IEnumerable<T> collection, int n)
        {
            return collection
                .Select((elem, idx) => (elem, idx))
                .GroupBy(x => x.idx / n)
                .Select(g => g.Select(x => x.elem).ToArray());
        }
    }
}
