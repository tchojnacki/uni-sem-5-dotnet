namespace Exercises
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Topic(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id, 2}) {Name, 20}";
        }
    }
}
