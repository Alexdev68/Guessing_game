using System.ComponentModel;

namespace Guessing_game.Models
{
    internal enum GameType
    {
        [Description("Easy mode: 3 numbers, 0-20, 6 attempts")]
        [DisplayText("Easy mode selected!")]
        [Prompt("Enter 3 numbers between 0 and 20, separated by spaces")]
        Easy = 1,

        [Description("Medium mode: 4 numbers, 0-50, 3 attempts")]
        [DisplayText("Medium mode selected!")]
        [Prompt("Enter 4 numbers between 0 and 50, separated by spaces")]
        Medium = 2,

        [Description("Hard mode: 5 mixed numbers, 0-90, 1 attempt")]
        [DisplayText("Hard mode selected!")]
        [Prompt("Enter 5 numbers between 0 and 100, separated by spaces")]
        Hard = 3,

        [Description("Random mode: Randomly selects one of the above modes")]
        Random = 99
    }
}