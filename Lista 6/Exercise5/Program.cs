// ReSharper disable StringLiteralTypo

using System;
using System.Text;

namespace Exercise5
{
    internal class Program
    {
        private static void Main()
        {
            // All params
            DrawCard("Ryszard", "Rys", 'X', 2, 20);

            // Only required param
            DrawCard("Andrzej");

            // Required param and some optional params
            DrawCard("Tomasz", "Chojnacki", '$');

            // All params using names
            DrawCard(
                firstLine: "Kamila",
                secondLine: "Kowalska",
                borderChar: '#',
                borderWidth: 3,
                minWidth: 10
            );

            // Some params using names
            DrawCard(firstLine: "Test", secondLine: "", minWidth: 40);

            // Mixed
            DrawCard("Piotr", "Lewandowski", borderWidth: 2, borderChar: '!');
        }

        private static void DrawCard(
            string firstLine,
            string? secondLine = null,
            char borderChar = 'X',
            int borderWidth = 1,
            int minWidth = 0
        )
        {
            if (borderWidth < 0)
                throw new ArgumentOutOfRangeException(nameof(borderWidth));
            if (minWidth < 0)
                throw new ArgumentOutOfRangeException(nameof(minWidth));

            var textWidth = Math.Max(firstLine.Length, secondLine?.Length ?? 0);
            var fullCardWidth = Math.Max(textWidth + 2 * borderWidth, minWidth);
            var innerWidth = fullCardWidth - 2 * borderWidth;

            var sideBorderString = new string(borderChar, borderWidth);
            var lineBorderString = new string(borderChar, fullCardWidth);

            var builder = new StringBuilder();

            for (var i = 0; i < borderWidth; i++)
                builder.AppendLine(lineBorderString);

            builder.Append(sideBorderString);
            builder.Append(CenterString(firstLine, innerWidth));
            builder.Append(sideBorderString);
            builder.AppendLine();

            if (secondLine is not null)
            {
                builder.Append(sideBorderString);
                builder.Append(CenterString(secondLine, innerWidth));
                builder.Append(sideBorderString);
                builder.AppendLine();
            }

            for (var i = 0; i < borderWidth; i++)
                builder.AppendLine(lineBorderString);

            Console.WriteLine(builder.ToString());
        }

        private static string CenterString(string input, int width) =>
            input.PadRight((width - input.Length) / 2 + input.Length).PadLeft(width);
    }
}
