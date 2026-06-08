using Guessing_game.Models;
using Guessing_game.Services;
using Guessing_game.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guessing_game
{
    internal static class GameEngine
    {
        static Random rand = new();
        public static void PlayGame()
        {
            "Welcome to Supreme Lotto👋".WriteDesigned(ConsoleColor.Red);

            GameConfig config = Menu.DisplayMenu();

            List<string> winningNumbers = RandomGenerator.Generate(config, rand);

            Console.WriteLine($"{string.Join(", ", winningNumbers)}");

            List<Player> players = PlayerService.CollectPlayers(config, winningNumbers);

            AnsiConsole.MarkupLine("\n\n[bold green]Game Started![/]");

            PlayerService.CollectGuesses(players, config, winningNumbers);

            AnsiConsole.MarkupLine($"\n[Gold1]Winning Numbers: {string.Join(" ", winningNumbers)}[/]");

            foreach (var player in players)
            {
                Validation.ValidatePlayer(winningNumbers, player, config);
            }

            if (config.AllowRollup)
            {
                players = ScoreManager.HandleRollup(players, config, rand);
            }

            ScoreManager.CalculateWinnings(players, config);

            List<Player> profiles = JsonStorageService.LoadPlayers();

            foreach (var gamePlayer in players)
            {
                var existingProfile = profiles.FirstOrDefault(p => p.Name == gamePlayer.Name);

                if (existingProfile != null)
                {
                    existingProfile.Score = gamePlayer.Score;
                    existingProfile.Winnings = gamePlayer.Winnings;
                    existingProfile.Balance = gamePlayer.Balance;
                    existingProfile.CorrectGuesses = gamePlayer.CorrectGuesses;
                }
                else
                {
                    profiles.Add(gamePlayer);
                }
            }

            JsonStorageService.UpdateStats(profiles, config);
            GameRecord history = new GameRecord
            {
                TimeStamp = DateTime.Now,
                GameId = Guid.NewGuid().ToString(),
                WinningNumbers = string.Join(", ", winningNumbers),
                Players = new List<PlayerResult>()
            };

            foreach (var p in players)
            {
                history.Players.Add(new PlayerResult
                {
                    Name = p.Name,
                    Score = p.Score,
                    Guesses = new List<string> { string.Join(", ", p.Guesses) }
                });
            }

            JsonStorageService.SaveHistory(history);

            LeaderBoard board = new();
            board.Display(players);

            "Thanks for playing!🙏".WriteDesigned(ConsoleColor.Green);
        }
    }
}
