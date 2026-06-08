using Guessing_game.Models;
using Guessing_game.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Guessing_game.Services
{
    internal class HistoryServices
    {
        public static void ViewAllHistory()
        {
            List<GameRecord> history = JsonStorageService.LoadHistory();

            foreach (var game in history)
            {
                Console.WriteLine();
                AnsiConsole.MarkupLine($"[yellow]Game ID: {game.GameId}[/]");
                AnsiConsole.MarkupLine($"[yellow]Date: {game.TimeStamp} [/]");
                AnsiConsole.MarkupLine($"[yellow]Winning Numbers: {string.Join(",", game.WinningNumbers)}[/]");

                foreach (var player in game.Players)
                {
                    AnsiConsole.MarkupLine($"\n[green]{player.Name}[/]");
                    AnsiConsole.MarkupLine($"[green]Score: {player.Score}[/]");
                    AnsiConsole.MarkupLine($"[green]Guesses:[/]");

                    foreach (var guess in player.Guesses)
                    {
                        AnsiConsole.MarkupLine($"[green]{string.Join(", ", guess)}[/]");
                    }
                }
            }
        }

        public static void FilterHistory()
        {
            List<GameRecord> history = JsonStorageService.LoadHistory();

            string str = "Start Date (YYYY-MM-DD): ".promptStyle();
            DateTime start = DateTime.Parse(str);

            str = "End Date (YYYY-MM-DD): ".promptStyle();
            DateTime end = DateTime.Parse(str);

            var filtered = history.Where(g => g.TimeStamp.Date >= start.Date && g.TimeStamp.Date <= end.Date);

            foreach  (var game in filtered)
            {
                Console.WriteLine();
                AnsiConsole.MarkupLine($"[yellow]Game ID: {game.GameId}[/]");
                AnsiConsole.MarkupLine($"[yellow]Date: {game.TimeStamp}[/]");
                AnsiConsole.MarkupLine($"[yellow]Winning Numbers: {string.Join(",", game.WinningNumbers)}[/]");

                foreach (var player in game.Players)
                {
                    AnsiConsole.MarkupLine($"\n[green]{player.Name}[/]");
                    AnsiConsole.MarkupLine($"[green]Score: {player.Score}[/]");
                    AnsiConsole.MarkupLine($"[green]Guesses:[/]");

                    foreach (var guess in player.Guesses)
                    {
                        AnsiConsole.MarkupLine($"[green]{string.Join(", ", guess)}[/]");
                    }
                }
            }
        }

        public static void ReplayBestRound()
        {
            List<GameRecord> history = JsonStorageService.LoadHistory();

            string str = "Enter player name: ".promptStyle();

            string name = str.Trim().ToLower();

            GameRecord? bestGame = null;

            int bestScore = -1;

            foreach (var game in history)
            {
                var player = game.Players.FirstOrDefault(p => p.Name.ToLower() == name);

                if (player == null)
                    continue;

                if (player.Score > bestScore)
                {
                    bestScore = player.Score;
                    bestGame = game;
                }
            }

            if (bestGame == null)
            {
                AnsiConsole.MarkupLine("[red]No game found for player.[/]");
                return;
            }

            AnsiConsole.MarkupLine("\n[gold1]========== BEST ROUND ==========[/]");
            AnsiConsole.MarkupLine($"[gold1]Game ID: {bestGame.GameId}[/]");
            AnsiConsole.MarkupLine($"[gold1]Date: {bestGame.TimeStamp}[/]");
            AnsiConsole.MarkupLine($"[gold1]Winning Numbers: {string.Join(", ", bestGame.WinningNumbers)}[/]\n");

            foreach (var player in bestGame.Players)
            {
                AnsiConsole.MarkupLine($"\n[gold1]{player.Name}[/]");
                AnsiConsole.MarkupLine($"[gold1]Score: {player.Score}[/]");
                AnsiConsole.MarkupLine("[gold1]Guesses:[/]");

                foreach (var guess in player.Guesses)
                {
                    AnsiConsole.MarkupLine($"[gold1]{string.Join(", ", guess)}[/]");
                }
            }

            AnsiConsole.MarkupLine("[gold1]===================================[/]\n");
        }
    }
}
