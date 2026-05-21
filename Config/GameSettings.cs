using Guessing_game.Models;
using System;

namespace Guessing_game.Config
{
    internal class GameSettings
    {
        public static GameConfig GetConfig(GameType type)
        {
            return type switch
            {
                GameType.Easy => new GameConfig
                {
                    Type = type,
                    Attempts = 6,
                    GuessLength = 3,
                    MinValue = 0,
                    MaxValue = 20,
                    Multiplier = 340,
                    AllowDuplicates = true,
                    AllowRollup = false,
                    AllowAlphanumeric = false
                },

                GameType.Medium => new GameConfig
                {
                    Type = type,
                    Attempts = 3,
                    GuessLength = 4,
                    MinValue = 0,
                    MaxValue = 50,
                    Multiplier = 2250,
                    AllowDuplicates = false,
                    AllowRollup = true,
                    AllowAlphanumeric = false
                },

                _ => new GameConfig
                {
                    Type = type,
                    Attempts = 1,
                    GuessLength = 5,
                    MinValue = 0,
                    MaxValue = 90,
                    Multiplier = 10000,
                    AllowDuplicates = true,
                    AllowRollup = true,
                    AllowAlphanumeric = true
                }
            };
        }
    }
}