using System;
using System.Collections.Generic;
using System.Text;

namespace Guessing_game.Models
{
    internal class PlayerResult
    {
        public string Name { get; set; }

        public List<string> Guesses { get; set; } = new();

        public int Score { get; set; }
    }
}
