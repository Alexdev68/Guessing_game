using Guessing_game.Models;
using System.Collections.Generic;

namespace Guessing_game.Services
{
    internal class Validation
    {
        public static void ValidatePlayer(List<string> winningNumbers, Player player, GameConfig config)
        {
            int matches = 0;

            foreach (string guess in player.Guesses)
            {
                if (winningNumbers.Contains(guess))
                {
                    matches++;
                }
            }

            player.CorrectGuesses = matches;

            player.Score = (matches * 100) / config.GuessLength;
        }
    }
}