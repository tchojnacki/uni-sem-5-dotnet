using LectureExamples;
using System.Collections.Generic;
using System.Linq;

namespace Exercises
{
    public class Student
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public List<int> TopicIds { get; set; }

        public Student(
            int id,
            int index,
            string name,
            Gender gender,
            bool active,
            int departmentId,
            List<int> topicIds
        )
        {
            Id = id;
            Index = index;
            Name = name;
            Gender = gender;
            Active = active;
            DepartmentId = departmentId;
            TopicIds = topicIds;
        }

        public override string ToString()
        {
            return $"{Id, 2}) {Index, 5}, {Name, 11}, {Gender, 6},{(Active ? "active" : "no active"), 9},{DepartmentId, 2}, topicIds: {string.Join(", ", TopicIds)}";
        }

        #region Equality

        private object EqualityMembers => (Id, Index, Name, Gender, Active, DepartmentId);

        protected bool Equals(Student other) =>
            EqualityMembers.Equals(other.EqualityMembers) && TopicIds.SequenceEqual(other.TopicIds);

        public override bool Equals(object obj) =>
            obj?.GetType() == GetType() && Equals(obj as Student);

        public override int GetHashCode() => EqualityMembers.GetHashCode();

        #endregion
    }
}
