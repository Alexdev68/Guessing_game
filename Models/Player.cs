using System.Text.Json.Serialization;

namespace Guessing_game.Models
{
    internal class Player
    {
        public string Name { get; set; }

        [JsonIgnore]
        public string[] Guesses { get; set; }

        public int CorrectGuesses { get; set; }

        public int Score { get; set; }

        public decimal Winnings { get; set; }

        public decimal Balance { get; set; } = 5000;

        [JsonIgnore]
        public decimal Stake { get; set; }

        public int GamesPlayed { get; set; }

        public int TotalWins { get; set; }

        public int BestScore { get; set; }

        public int TotalScore { get; set; }

        public double AverageScore => GamesPlayed == 0 ? 0 : (double)TotalScore / GamesPlayed;

        public DateTime FirstSeen { get; set; }

        public DateTime LastSeen { get; set; }
    }
}