using System.Collections.Generic;
using System.Linq;

namespace LectureExamples
{
    public class StudentWithTopics
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public List<string> Topics { get; set; }

        public StudentWithTopics(
            int id,
            int index,
            string name,
            Gender gender,
            bool active,
            int departmentId,
            List<string> topics
        )
        {
            Id = id;
            Index = index;
            Name = name;
            Gender = gender;
            Active = active;
            DepartmentId = departmentId;
            Topics = topics;
        }

        public override string ToString()
        {
            var result =
                $"{Id, 2}) {Index, 5}, {Name, 11}, {Gender, 6},{(Active ? "active" : "no active"), 9},{DepartmentId, 2}, topics: ";
            return Topics.Aggregate(result, (current, str) => current + str + ", ");
        }
    }
}
