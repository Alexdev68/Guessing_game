using Guessing_game.Models;
using Guessing_game.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Guessing_game.Services
{
    internal class ScoreManager
    {
        public static void CalculateWinnings(List<Player> players, GameConfig config)
        {
            foreach (Player player in players)
            {
                int multiplier = config.Multiplier;

                if (player.Score < 50)
                {
                    multiplier = 0;
                }
                else if (player.Score < 100)
                {
                    multiplier /= 2;
                }

                player.Winnings = player.Stake * multiplier;

                player.Balance += player.Winnings;
            }
        }

        public static List<Player> HandleRollup(List<Player> players, GameConfig config, Random rand)
        {
            int passScore = ((config.GuessLength - 1) * 100) / config.GuessLength;

            while (true)
            {
                List<Player> passedPlayers = players.Where(p => p.Score >= passScore).ToList();

                if (passedPlayers.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red]No players passed.[/]");

                    return players;
                }

                int highestScore = passedPlayers.Max(p => p.Score);

                List<Player> topPlayers = passedPlayers.Where(p => p.Score == highestScore).ToList();

                if (topPlayers.Count == 1)
                {
                    AnsiConsole.MarkupLine($"\n[bold green]{topPlayers[0].Name} wins with a score of {topPlayers[0].Score}![/]");

                    return players;
                }

                AnsiConsole.MarkupLine("\n[bold yellow]ROLLUP ACTIVATED![/]");

                List<string> newWinningNumbers = RandomGenerator.Generate(config, rand);

                AnsiConsole.MarkupLine("\n[bold gold1]New Winning Numbers generated[/]");

                foreach (Player player in topPlayers)
                {
                    string guess = $"\n{player.Name}, enter new guesses:".promptStyle();

                    player.Guesses = GuessParser.ParseGuesses(guess, config);

                    player.GuessHistory.Add(player.Guesses);

                    Validation.ValidatePlayer(newWinningNumbers, player, config);
                }
                AnsiConsole.MarkupLine($"\n[bold gold1]New Winning Numbers were: { string.Join(" ", newWinningNumbers)}[/]");
                players = topPlayers;
            }
        }

    }
}