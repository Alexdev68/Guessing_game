using Guessing_game.Models;
using System.Collections.Generic;

namespace Guessing_game.Services
{
    internal class Validation
    {
        public static void ValidatePlayer(List<string> winningNumbers, Player player, GameConfig config)
        {
            int matches = 0;
            var sourceCounts = new Dictionary<string, int>(winningNumbers.Count);

            if (player.Guesses.Count == 0)
                return;

            string lastGuess = player.Guesses.Last();

            List<string> guesses = lastGuess.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(g => g.Trim()).ToList();

            foreach (string item in winningNumbers)
            {
                if (sourceCounts.TryGetValue(item, out int count))
                {
                    sourceCounts[item] = count + 1;
                }
                else
                {
                    sourceCounts[item] = 1;
                }
            }

            foreach (string guess in guesses)
            {
                string key = config.AllowAlphanumeric ? guess.ToUpperInvariant() : guess;

                if (sourceCounts.TryGetValue(key, out int count) && count > 0)
                {
                    matches++;
                    sourceCounts[key] = count - 1;
                }
            }

            player.CorrectGuesses = matches;

            player.Score = (matches * 100) / config.GuessLength;
        }
    }
}