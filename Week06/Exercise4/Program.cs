using System;

namespace Exercise4
{
    internal class Program
    {
        private static void Main()
        {
            var nameObject = new { FirstName = "Tomasz", LastName = "Chojnacki", Age = 21 };
            MethodTakingAnonymousTypeByDynamic(nameObject);
            MethodTakingAnonymousTypeWithReflection(nameObject);
        }

        private static void MethodTakingAnonymousTypeByDynamic(dynamic nameObj)
        {
            Console.WriteLine($"Name: {nameObj.FirstName} {nameObj.LastName}, age: {nameObj.Age}");
        }

        private static void MethodTakingAnonymousTypeWithReflection(object nameObj)
        {
            var type = nameObj.GetType();

            var firstName = (string)type.GetProperty("FirstName")?.GetValue(nameObj);
            var lastName = (string)type.GetProperty("LastName")?.GetValue(nameObj);
            var age = (int?)type.GetProperty("Age")?.GetValue(nameObj);

            Console.WriteLine($"Name: {firstName} {lastName}, age: {age}");
        }
    }
}
