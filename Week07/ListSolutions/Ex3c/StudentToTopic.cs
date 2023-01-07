namespace Exercises.Ex3c
{
    public class StudentToTopic
    {
        public int StudentId { get; set; }
        public int TopicId { get; set; }

        public StudentToTopic(int studentId, int topicId)
        {
            StudentId = studentId;
            TopicId = topicId;
        }

        public override string ToString()
        {
            return $"STT(studentId: {StudentId, 2}, topicId: {TopicId, 2})";
        }
    }
}
