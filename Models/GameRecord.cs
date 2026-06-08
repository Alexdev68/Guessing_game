using System;
using System.Collections.Generic;
using System.Text;

namespace Guessing_game.Models
{
    internal class GameRecord
    {
        public string GameId { get; set; }

        public DateTime TimeStamp { get; set; }

        public string WinningNumbers { get; set; }

        public List<PlayerResult> Players { get; set; }
    }
}
