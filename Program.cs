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
                        GameEngine.PlayGame();
                        break;

                    case "2":
                        HistoryServices.ViewAllHistory();
                        break;

                    case "3":
                        HistoryServices.FilterHistory();
                        break;

                    case "4":
                        HistoryServices.ReplayBestRound();
                        break;

                    case "5":
                        return;
                }
            }
        }
    }
}