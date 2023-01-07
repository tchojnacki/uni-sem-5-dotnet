using LectureExamples;

namespace Exercises.Ex3c
{
    public class Student
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public Student(int id, int index, string name, Gender gender, bool active, int departmentId)
        {
            Id = id;
            Index = index;
            Name = name;
            Gender = gender;
            Active = active;
            DepartmentId = departmentId;
        }

        public override string ToString()
        {
            return $"{Id, 2}) {Index, 5}, {Name, 11}, {Gender, 6},{(Active ? "active" : "no active"), 9},{DepartmentId, 2}";
        }
    }
}
