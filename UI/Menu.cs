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

                AnsiConsole.MarkupLine($"[green]{(int)GameType.Easy} - ({GameType.Easy.Description()})[/]");
                AnsiConsole.MarkupLine($"[yellow]{(int)GameType.Medium} - ({GameType.Medium.Description()})[/]");
                AnsiConsole.MarkupLine($"[red]{(int)GameType.Hard} -  ({GameType.Hard.Description()})[/]");
                AnsiConsole.MarkupLine($"[magenta]{(int)GameType.Random} - Random[/]");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    continue;
                }
                GameType gameType = (GameType)choice;

                switch (gameType)
                {
                    case GameType.Easy:
                        AnsiConsole.MarkupLine($"\n[green]{GameType.Easy.DisplayText()}[/]");
                        return GameSettings.GetConfig(gameType);
                    case GameType.Medium:
                        AnsiConsole.MarkupLine($"\n[yellow]{GameType.Medium.DisplayText()}[/]");
                        return GameSettings.GetConfig(gameType);
                    case GameType.Hard:
                        AnsiConsole.MarkupLine($"\n[red]{GameType.Hard.DisplayText()}[/]");
                        return GameSettings.GetConfig(gameType);
                    case GameType.Random:
                        int type = new Random().Next(0, 3);

                        if (type == 0)
                            AnsiConsole.MarkupLine($"\n[green]{GameType.Random.DisplayText()}(Easy)[/]");
                        else if (type == 1)
                            AnsiConsole.MarkupLine($"\n[yellow]{GameType.Random.DisplayText()}(Medium)[/]");
                        else
                            AnsiConsole.MarkupLine($"\n[red]{GameType.Random.DisplayText()}(Hard)[/]");
                        return GameSettings.GetConfig((GameType) type);
                    default:
                        return null;
                }
            }
        }
    }
}