namespace Guessing_game.Models
{
    internal class Player
    {
        public string Name { get; set; }

        public string[] Guesses { get; set; }

        public int CorrectGuesses { get; set; }

        public int Score { get; set; }

        public decimal Winnings { get; set; }

        public decimal Balance { get; set; } = 5000;

        public decimal Stake { get; set; }

        public List<string[]> GuessHistory { get; set; } = new();
    }
}