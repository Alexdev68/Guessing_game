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

                foreach (var keyValuePair in GameSettings.AllGames)
                {
                    var type = keyValuePair.Key;
                    var config = keyValuePair.Value;

                    AnsiConsole.MarkupLine($"[bold {config.color}]{(int)type} - {type.Description()}[/]");
                }

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    continue;
                }
                var gameType = (GameType)choice;
                
                
                AnsiConsole.MarkupLine($"\n[green]{gameType.DisplayText()}[/]");
                return GameSettings.GetConfig(gameType);
            }
        }
    }
}