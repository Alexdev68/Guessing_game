using Guessing_game.Models;
using Guessing_game.Services;
using Guessing_game.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal static class PlayerService
    {
        public static List<Player> CollectPlayers(GameConfig config, List<string> winningNumbers)
        {
            List<Player> players = new();
            int playerCount = 0;

            int passScore = ((config.GuessLength - 1) * 100) / config.GuessLength;

            while (true)
            {
                AnsiConsole.MarkupLine("[bold yellow]Enter Number of players (min 2, max 10): [/]");
                playerCount = Convert.ToInt32(Console.ReadLine());

                if (playerCount < 2 || playerCount > 10)
                {
                    AnsiConsole.MarkupLine("[red]Number of players must be between 2 and 10[/]");
                }
                else
                    break;
            }

            AnsiConsole.MarkupLine("\n[bold cyan]         Player Registration[/]");
            AnsiConsole.MarkupLine("[bold cyan]-------------------------------------[/]");
            for (int i = 0; i < playerCount; i++)
            {
                string name = "\nEnter player name: ".promptStyle();

                if (string.IsNullOrWhiteSpace(name))
                    continue;

                Player player = new Player
                {
                    Name = name,
                    Balance = 5000
                };

                decimal stake = GetStake();

                player.Stake = stake;
                player.Balance -= stake;

                players.Add(player);

                AnsiConsole.MarkupLine($"[green]✔️ Balance left: ₦{player.Balance}[/]");
            }

            return players;
        }

        public static decimal GetStake()
        {
            while (true)
            {
                string stake = "[cyan]Enter stake amount: [/]".promptStyle();

                if (decimal.TryParse(stake, out decimal amount))
                {
                    return amount;
                }

                AnsiConsole.MarkupLine("[red]Invalid amount.[/]");
            }
        }

        public static void CollectGuesses(List<Player> players, GameConfig config, List<string> winningNumbers)
        {
            foreach (var player in players)
            {
                AnsiConsole.MarkupLine($"\n\n[bold cyan]🎲{player.Name}'s Turn[/]");
                for (int attempt = 0; attempt < config.Attempts; attempt++)
                {
                    AnsiConsole.MarkupLine($"[cyan]\n Attempt {attempt + 1}/{config.Attempts}[/]");
                    if (config.Type == GameType.Easy)
                        AnsiConsole.MarkupLine("[yellow]Enter 3 numbers between 1 and 20, separated by spaces[/]");
                    else if (config.Type == GameType.Medium)
                        AnsiConsole.MarkupLine("[yellow]Enter 4 numbers between 1 and 50, separated by spaces[/]");
                    else
                        AnsiConsole.MarkupLine("[yellow]Enter 5 numbers between 1 and 100, separated by spaces[/]");

                    string guess = $"[cyan]Enter {config.GuessLength} guesses: [/]".promptStyle();

                    player.Guesses = GuessParser.ParseGuesses(guess, config);

                    Validation.ValidatePlayer(winningNumbers, player, config);

                    player.GuessHistory.Add((string[])player.Guesses.Clone());

                    if (player.Score >= ((config.GuessLength - 1) * 100) / config.GuessLength)
                    {
                        AnsiConsole.MarkupLine($"[green]Congratulations {player.Name}![/] You scored {player.Score}%.");
                        break;
                    }
                    AnsiConsole.MarkupLine($"[bold red]FAIL![/] [red]You have {config.Attempts - attempt - 1} attempts left.[/]");
                }
            }
        }
    }
}