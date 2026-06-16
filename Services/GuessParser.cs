using Guessing_game.Models;
using Guessing_game.UI;
using Spectre.Console;
using System;
using System.Linq;

namespace Guessing_game.Services
{
    internal class GuessParser
    {
        public static string ParseGuesses(string input, GameConfig config)
        {
            while (true)
            {
                string[] guesses = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (guesses.Length != config.GuessLength)
                {
                    input = $"Enter exactly {config.GuessLength} values.".promptStyle();

                    continue;
                }

                bool valid = true;

                foreach (string guess in guesses)
                {
                    if (config.AllowAlphanumeric)
                    {
                        bool isNumber = int.TryParse(guess, out int num);

                        if (isNumber)
                        {
                            if (num < config.MinValue || num > config.MaxValue)
                            {
                                valid = false;
                            }
                        }
                        else if (guess.Length != 1 || !char.IsLetter(guess[0]))
                        {
                            valid = false;
                        }
                    }
                    else
                    {
                        if (!int.TryParse(guess, out int num) || num < config.MinValue || num > config.MaxValue)
                        {
                            valid = false;
                        }
                    }
                }

                if (!config.AllowDuplicates && guesses.Distinct().Count() != guesses.Length)
                {
                    AnsiConsole.MarkupLine("[red]Duplicates are not allowed.[/]");

                    valid = false;
                }

                if (valid)
                    return string.Join(", ", guesses);

                input = "[red]Invalid input. Try again: [/]".promptStyle();
            }
        }
    }
}