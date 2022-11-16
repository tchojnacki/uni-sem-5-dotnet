// ReSharper disable CommentTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable ArrangeVarKeywordsInDeconstructingDeclaration

using System;

namespace Exercise1
{
    internal class Program
    {
        private static void Main()
        {
            var nameTuple = (FirstName: "Tomasz", LastName: "Chojnacki", Age: 21);
            MethodTakingATuple(nameTuple);
        }

        private static void MethodTakingATuple(
            (string FirstName, string LastName, int Age) nameTuple
        )
        {
            // 0) Wypisanie krotki z domyślnym formatowaniem
            Console.WriteLine(nameTuple);

            // 1) Przepisywanie krotki do osobno zadeklarowanych zmiennych
            (string firstName1, string lastName1, int age1) = nameTuple;
            Console.WriteLine($"First name: {firstName1}, last name: {lastName1}, age: {age1}");

            // 2) Przepisywanie krotki do osobno deklarowanych wcześniej zmiennych
            string firstName2;
            string lastName2;
            int age2;
            (firstName2, lastName2, age2) = nameTuple;
            Console.WriteLine($"Name: {firstName2} {lastName2}, age: {age2}");

            // 3) Przepisywanie krotki do osobno deklarowanych zmiennych z niejawnie określonym typem
            (var firstName3, var lastName3, var age3) = nameTuple;
            Console.WriteLine($"Name: {firstName3} {lastName3}, age: {age3}");

            // 4) Przepisywanie krotki do osobno deklarowanych zmiennych z niejawnie określonym typem wszystkich zmiennych
            var (firstName4, lastName4, age4) = nameTuple;
            Console.WriteLine($"Name: {firstName4} {lastName4}, age: {age4}");

            // 5) Dostęp do elementów za pomocą nazw nadanych
            Console.WriteLine(
                $"Name: {nameTuple.FirstName} {nameTuple.LastName}, age: {nameTuple.Age}"
            );

            // 6) Dostęp do elementów za pomocą nazw domyślnych
            Console.WriteLine($"Name: {nameTuple.Item1} {nameTuple.Item2}, age: {nameTuple.Item3}");
        }
    }
}
