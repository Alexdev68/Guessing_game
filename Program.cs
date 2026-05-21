using Guessing_game.Models;
using Guessing_game.Services;
using Guessing_game.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class BabaIjebu
    {
        static Random rand = new();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            "Welcome to Supreme Lotto👋".WriteDesigned(ConsoleColor.Red);

            GameConfig config = Menu.DisplayMenu();

            List<string> winningNumbers = RandomGenerator.Generate(config, rand);

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

            LeaderBoard board = new();
            board.Display(players);

            "Thanks for playing!🙏".WriteDesigned(ConsoleColor.Green);
        }
    }
}