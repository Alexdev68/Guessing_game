using Guessing_game.Models;
using System.Collections.Generic;

namespace Guessing_game.Services
{
    internal class Validation
    {
        public static void ValidatePlayer(List<string> winningNumbers, Player player, GameConfig config)
        {
            int matches = 0;
            string gue;

            foreach (string guess in player.Guesses)
            {
                string normalized = config.AllowAlphanumeric ? guess.ToUpperInvariant() : guess;
                
                if (winningNumbers.Contains(normalized))
                    matches++;
            }

            player.CorrectGuesses = matches;

            player.Score = (matches * 100) / config.GuessLength;
        }
    }
}