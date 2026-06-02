using Spectre.Console;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Guessing_game.UI
{
    public static class ConsoleExtensions
    {
        public static void WriteDesigned(this string text, ConsoleColor color)
        {
            int width = 48;

            string str = new string('═', width);
            string topBorder = "╔" + str + "╗";
            string bottomBorder = "╚" + str + "╝";

            int left = (Console.WindowWidth - width) / 2;

            left = Math.Max(0, left);

            string pad = new string(' ', left);

            int innerWidth = width - 2;

            int textPadding = (innerWidth - text.Length) / 2;

            string middle = "║" + text.PadLeft(text.Length + textPadding).PadRight(innerWidth) + "  ║";

            Console.ForegroundColor = color;

            Console.WriteLine(pad + topBorder);
            Console.WriteLine(pad + middle);
            Console.WriteLine(pad + bottomBorder);

            Console.ResetColor();
        }

        public static string promptStyle(this string text)
        {
            return AnsiConsole.Prompt(new TextPrompt<string>($"[cyan]{text}[/]").PromptStyle("cyan"));
        }

        public static string Description(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? value.ToString();
        }

        public static string DisplayText(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttribute<DisplayTextAttribute>();
            return attr?.Text ?? value.ToString();
        }

        public static string Prompt(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttribute<PromptAttribute>();
            return attr?.Prompt ?? value.ToString();
        }
    }
}