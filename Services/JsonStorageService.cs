using Guessing_game.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Guessing_game.Services
{
    internal static class JsonStorageService
    {
        private const string PlayersFile = "C:\\Users\\Ikechukwuanachebe\\source\\repos\\Guessing game\\Players.json";
        private const string HistoryFile = "C:\\Users\\Ikechukwuanachebe\\source\\repos\\Guessing game\\GameHistory.json";

        public static List<Player> LoadPlayers()
        {
            if (!File.Exists(PlayersFile))
                return new List<Player>();

            string json = File.ReadAllText(PlayersFile);

            if (string.IsNullOrWhiteSpace(json))
            {
                AnsiConsole.MarkupLine("[bold red]NO history available, file is empty[/]");
                return new List<Player>();
            }

            return JsonSerializer.Deserialize<List<Player>>(json) ?? new List<Player>();
        }

        public static void SavePlayers(List<Player> players)
        {
            string json = JsonSerializer.Serialize(players, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(PlayersFile, json);
        }

        public static List<GameRecord> LoadHistory()
        {
            if (!File.Exists(HistoryFile))
                return new List<GameRecord>();

            string json = File.ReadAllText(HistoryFile);

            if (string.IsNullOrWhiteSpace(json))
            {
                AnsiConsole.MarkupLine("[bold red]NO history available, file is empty[/]");
                return new List<GameRecord>();
            }

            return JsonSerializer.Deserialize<List<GameRecord>>(json)
                   ?? new List<GameRecord>();
        }

        public static void SaveHistory(GameRecord history)
        {
            if (!File.Exists(HistoryFile))
            {
                File.WriteAllText(HistoryFile, "[]");
            }

            string json = File.ReadAllText(HistoryFile);

            if (string.IsNullOrWhiteSpace(json))
            {
                json = "[]";
            }

            List<GameRecord> historyList = JsonSerializer.Deserialize<List<GameRecord>>(json) ?? new List<GameRecord>();

            historyList.Add(history);

            string updatedJson = JsonSerializer.Serialize(historyList, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(HistoryFile, updatedJson);
        }

        public static void UpdateStats(List<Player> profiles, GameConfig config)
        {
            int highestScore = profiles.Max(p => p.Score);

            foreach (var player in profiles)
            {
                player.GamesPlayed++;
                player.TotalScore += player.Score;
                player.BestScore = Math.Max(player.BestScore, player.Score);
                player.LastSeen = DateTime.Now;

                if (player.Score == highestScore && player.CorrectGuesses >= config.GuessLength - 1)
                    player.TotalWins++;
            }

            SavePlayers(profiles);
        }
    }
}
