// ReSharper disable StringLiteralTypo

using System.Collections.Generic;

namespace LectureExamples
{
    public static class Generator
    {
        public static List<StudentWithTopics> GenerateStudentsWithTopicsEasy()
        {
            return new List<StudentWithTopics>
            {
                new(
                    1,
                    12345,
                    "Nowak",
                    Gender.Female,
                    true,
                    1,
                    new List<string> { "C#", "PHP", "algorithms" }
                ),
                new(
                    2,
                    13235,
                    "Kowalski",
                    Gender.Male,
                    true,
                    1,
                    new List<string> { "C#", "C++", "fuzzy logic" }
                ),
                new(
                    3,
                    13444,
                    "Schmidt",
                    Gender.Male,
                    false,
                    2,
                    new List<string> { "Basic", "Java" }
                ),
                new(
                    4,
                    14000,
                    "Newman",
                    Gender.Female,
                    false,
                    3,
                    new List<string> { "JavaScript", "neural networks" }
                ),
                new(5, 14001, "Bandingo", Gender.Male, true, 3, new List<string> { "Java", "C#" }),
                new(
                    6,
                    14100,
                    "Miniwiliger",
                    Gender.Male,
                    true,
                    2,
                    new List<string> { "algorithms", "web programming" }
                ),
                new(
                    11,
                    22345,
                    "Nowak",
                    Gender.Female,
                    true,
                    2,
                    new List<string> { "C#", "PHP", "algorithms" }
                ),
                new(
                    12,
                    23235,
                    "Nowak",
                    Gender.Male,
                    true,
                    1,
                    new List<string> { "C#", "C++", "fuzzy logic" }
                ),
                new(
                    13,
                    23444,
                    "Schmidt",
                    Gender.Male,
                    false,
                    1,
                    new List<string> { "Basic", "Java" }
                ),
                new(
                    14,
                    24000,
                    "Newman",
                    Gender.Female,
                    false,
                    1,
                    new List<string> { "JavaScript", "neural networks" }
                ),
                new(15, 24001, "Bandingo", Gender.Male, true, 3, new List<string> { "Java", "C#" }),
                new(
                    16,
                    24100,
                    "Bandingo",
                    Gender.Male,
                    true,
                    2,
                    new List<string> { "algorithms", "web programming" }
                )
            };
        }
    }
}
