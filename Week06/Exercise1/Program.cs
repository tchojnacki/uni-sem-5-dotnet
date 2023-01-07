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
            var nameTuple = (
                FirstName: "Tomasz",
                LastName: "Chojnacki",
                Age: 21,
                Salary: 2_800.50m
            );
            MethodTakingATuple(nameTuple);
        }

        private static void MethodTakingATuple(
            (string FirstName, string LastName, int Age, decimal Salary) nameTuple
        )
        {
            // 0) Wypisanie krotki z domyślnym formatowaniem
            Console.WriteLine(nameTuple);

            // 1) Przepisywanie krotki do osobno zadeklarowanych zmiennych
            (string firstName1, string lastName1, int age1, decimal salary1) = nameTuple;
            Console.WriteLine(
                $"Name: {firstName1} {lastName1}, age: {age1}, salary: {salary1} PLN"
            );

            // 2) Przepisywanie krotki do osobno deklarowanych wcześniej zmiennych
            string firstName2;
            string lastName2;
            int age2;
            decimal salary2;
            (firstName2, lastName2, age2, salary2) = nameTuple;
            Console.WriteLine(
                $"Name: {firstName2} {lastName2}, age: {age2}, salary: {salary2} PLN"
            );

            // 3) Przepisywanie krotki do osobno deklarowanych zmiennych z niejawnie określonym typem, pominięcie elementu
            (var firstName3, var lastName3, _, var salary3) = nameTuple;
            Console.WriteLine($"Name: {firstName3} {lastName3}, salary: {salary3} PLN");

            // 4) Przepisywanie krotki do osobno deklarowanych zmiennych z niejawnie określonym typem wszystkich zmiennych
            var (firstName4, lastName4, age4, salary4) = nameTuple;
            Console.WriteLine(
                $"Name: {firstName4} {lastName4}, age: {age4}, salary: {salary4} PLN"
            );

            // 5) Dostęp do elementów za pomocą nazw nadanych
            Console.WriteLine(
                $"Name: {nameTuple.FirstName} {nameTuple.LastName}, age: {nameTuple.Age}, salary: {nameTuple.Salary} PLN"
            );

            // 6) Dostęp do elementów za pomocą nazw domyślnych
            Console.WriteLine(
                $"Name: {nameTuple.Item1} {nameTuple.Item2}, age: {nameTuple.Item3}, salary: {nameTuple.Item4} PLN"
            );
        }
    }
}
