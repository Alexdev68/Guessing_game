using Guessing_game.Config;
using Guessing_game.Models;
using Spectre.Console;
using System;

namespace Guessing_game.UI
{
    internal class Menu
    {
        public static GameConfig DisplayMenu()
        {
            while (true)
            {
                AnsiConsole.MarkupLine("\n[bold cyan]Select Game Mode:[/]");

                AnsiConsole.MarkupLine("[green]1 - Easy[/]   (3 numbers, 0-20, 6 attempts)");
                AnsiConsole.MarkupLine("[yellow]2 - Medium[/] (4 numbers, 0-50, 3 attempts)");
                AnsiConsole.MarkupLine("[red]3 - Hard[/]   (5 mixed, 0-90, 1 attempt)");
                AnsiConsole.MarkupLine("[magenta]4 - Random[/]");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AnsiConsole.MarkupLine("\n[green]Easy mode selected![/]");
                        return GameSettings.GetConfig(GameType.Easy);
                    case 2:
                        AnsiConsole.MarkupLine("\n[yellow]Medium mode selected![/]");
                        return GameSettings.GetConfig(GameType.Medium);
                    case 3:
                        AnsiConsole.MarkupLine("\n[red]Hard mode selected![/]");
                        return GameSettings.GetConfig(GameType.Hard);
                    case 4:
                        int type = new Random().Next(0, 3);

                        if (type == 0)
                            AnsiConsole.MarkupLine("\n[green]Random mode selected! (Easy)[/]");
                        else if (type == 1)
                            AnsiConsole.MarkupLine("\n[yellow]Random mode selected! (Medium)[/]");
                        else
                            AnsiConsole.MarkupLine("\n[red]Random mode selected! (Hard)[/]");
                        return GameSettings.GetConfig((GameType) type);
                    default:
                        return null;
                }
            }
        }
    }
}