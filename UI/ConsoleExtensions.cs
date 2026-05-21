using Spectre.Console;
using System;

namespace Guessing_game.UI
{
    public static class ConsoleExtensions
    {
        public static void WriteDesigned(this string text, ConsoleColor color)
        {
            int width = 50;

            string border = new string('*', width);

            int left = (Console.WindowWidth - width) / 2;

            left = Math.Max(0, left);

            string pad = new string(' ', left);

            int innerWidth = width - 2;

            int textPadding = (innerWidth - text.Length) / 2;

            string middle = "*" + text.PadLeft( text.Length + textPadding) .PadRight(innerWidth) + "*";

            Console.ForegroundColor = color;

            Console.WriteLine(pad + border);
            Console.WriteLine(pad + middle);
            Console.WriteLine(pad + border);

            Console.ResetColor();
        }

        public static string promptStyle(this string text)
        {
            return AnsiConsole.Prompt(new TextPrompt<string>($"[cyan]{text}[/]").PromptStyle("cyan"));
        }
    }
}