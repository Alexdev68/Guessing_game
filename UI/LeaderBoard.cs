using Guessing_game.Models;
using Spectre.Console;
using System.Collections.Generic;
using System.Linq;

namespace Guessing_game.UI
{
    internal class LeaderBoard
    {
        public void Display(
            List<Player> players)
        {
            Table table = new();

            table.Title =
                new TableTitle("[bold cyan]🏆 Leaderboard 🏆[/]");

            table.AddColumn("Rank");
            table.AddColumn("Player");
            table.AddColumn("Score");
            table.AddColumn("Winnings");

            var sortedPlayers = players.OrderByDescending(p => p.Score);

            int rank = 1;

            foreach (var player in sortedPlayers)
            {
                table.AddRow(rank.ToString(), player.Name, player.Score + "%", $"₦{player.Winnings}");

                rank++;
            }

            AnsiConsole.Write(table);
        }
    }
}