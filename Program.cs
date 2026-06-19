using Guessing_game.Models;
using Guessing_game.Services;
using Guessing_game.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guessing_game
{
    internal class BabaIjebu
    {
        static Random rand = new();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                AnsiConsole.MarkupLine($"[Bold green]{new string('═', 20)}[/]");
                AnsiConsole.MarkupLine("[green]1. Play Game[/]");
                AnsiConsole.MarkupLine("[green]2. View History[/]");
                AnsiConsole.MarkupLine("[green]3. Filter History[/]");
                AnsiConsole.MarkupLine("[green]4. Replay best round[/]");
                AnsiConsole.MarkupLine("[green]5. Exit[/]");
                AnsiConsole.MarkupLine($"[Bold green]{new string('═', 20)}[/]");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        GameEngine.PlayGame();
                        AnsiConsole.Markup("\n[cyan]Press any key to display menu . . .[/]");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;

                    case "2":
                        Console.Clear();
                        HistoryServices.ViewAllHistory();
                        AnsiConsole.Markup("\n[cyan]Press any key to display menu . . .[/]");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;

                    case "3":
                        Console.Clear();
                        HistoryServices.FilterHistory();
                        AnsiConsole.Markup("\n[cyan]Press any key to display menu . . .[/]");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;

                    case "4":
                        Console.Clear();
                        HistoryServices.ReplayBestRound();
                        AnsiConsole.Markup("\n[cyan]Press any key to display menu . . .[/]");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;

                    case "5":
                        return;
                }
            }
        }
    }
}