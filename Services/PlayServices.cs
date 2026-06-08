using Guessing_game.Models;
using Guessing_game.Services;
using Guessing_game.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guessing_game.Services
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
                AnsiConsole.MarkupLine($"[bold yellow]Enter Number of players (min {config.MinPlayers}, max {config.MaxPlayers}): [/]");

                if (!int.TryParse(Console.ReadLine(), out playerCount))
                {
                    if (playerCount < config.MinPlayers || playerCount > config.MaxPlayers)
                    {
                        AnsiConsole.MarkupLine($"[red]Number of players must be an integer between {config.MinPlayers} and {config.MaxPlayers}[/]");
                    }
                }
                else
                    break;
            }

            List<Player> profiles = JsonStorageService.LoadPlayers();

            AnsiConsole.MarkupLine("\n[bold cyan]         Player Registration[/]");
            AnsiConsole.MarkupLine("[bold cyan]-------------------------------------[/]");
            for (int i = 0; i < playerCount; i++)
            {
                string name = "\nEnter player name: ".promptStyle();

                if (string.IsNullOrWhiteSpace(name))
                    continue;

                string normalizedName = name.Trim().ToLower();

                Player? player = profiles.FirstOrDefault(p => p.Name.ToLower() == normalizedName);

                if (player == null)
                {
                    player = new Player
                    {
                        Name = normalizedName,
                        Balance = 5000,
                        FirstSeen = DateTime.Now
                    };

                    profiles.Add(player);

                    JsonStorageService.SavePlayers(profiles);
                }
                else
                {
                    AnsiConsole.MarkupLine($"[bold yellow]Welcome back {normalizedName}[/]");
                    player.LastSeen = DateTime.Now;
                    player.Score = 0;
                    player.CorrectGuesses = 0;
                    player.Winnings = 0;
                    player.Guesses = Array.Empty<string>();
                }

                decimal stake = GetStake();


                if (player.Balance < stake)
                {
                    AnsiConsole.MarkupLine("[bold red]Insufficient balance[/]");

                    while (true)
                    {
                        string str = "[cyan]How much would you like to deposit:[/]".promptStyle();

                        if (decimal.TryParse(str, out decimal balance))
                        {
                            player.Balance += balance;
                            break;
                        }

                        AnsiConsole.MarkupLine("[bold red]Deposit must be an integer ₦" + stake + "[/]");
                    }
                }

                player.Stake = stake;
                player.Balance -= stake;

                players.Add(player);

                AnsiConsole.MarkupLine($"[green]✔️ Balance left: \u20A6{player.Balance}[/]");
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
                    AnsiConsole.MarkupLine($"[yellow]{config.Type.Prompt()}[/]");

                    string guess = $"[cyan]Enter {config.GuessLength} guesses: [/]".promptStyle();

                    player.Guesses = GuessParser.ParseGuesses(guess, config);

                    Validation.ValidatePlayer(winningNumbers, player, config);

                    if (player.Score >= ((config.GuessLength - 1) * 100) / config.GuessLength)
                    {
                        AnsiConsole.MarkupLine($"[bold green]Congratulations {player.Name}![/] [green]You scored {player.Score}%.[/]");
                        break;
                    }
                    AnsiConsole.MarkupLine($"[bold red]FAIL![/] [red]You have {config.Attempts - attempt - 1} attempts left.[/]");
                }
            }
        }
    }
}