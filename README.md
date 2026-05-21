# 🎰 Supreme Lotto — Baba Ijebu Style Guessing Game
 
A multiplayer console-based lottery and number guessing game built with C#.
 
This project simulates a Baba Ijebu-style lottery system where multiple players can:
- choose different game modes,
- place stakes,
- enter guesses,
- compete on a leaderboard,
- and participate in rollup rounds until a winner emerges.
 
---
 
# 📌 Features
 
## ✅ Multiple Players
- Minimum: 2 players
- Maximum: 10 players
 
---
 
## ✅ Multiple Game Modes
 
### 🟢 Easy Mode
- Smaller number range
- More attempts
- Duplicates allowed
- No rollup
 
### 🟡 Medium Mode
- Larger range
- Fewer attempts
- No duplicates
- Rollup enabled
 
### 🔴 Hard Mode
- Alphanumeric guesses
- Very high multiplier
- Few attempts
- Rollup enabled
 
---
 
## ✅ Rollup System
If multiple players tie with the highest passing score:
- the game continues only for tied players,
- new winning numbers are generated,
- and the process repeats until one winner remains.
 
---
 
## ✅ Attempt System
Players can retry guesses depending on the game mode configuration.
 
Attempts stop immediately when:
- the player reaches the passing threshold.
 
---
 
## ✅ Leaderboard
At the end of the game:
- players are ranked from highest to lowest score,
- winnings are displayed,
- and medals are awarded to top players.
 
---
 
# 🛠 Technologies Used
 
- C#
- .NET
- Spectre.Console
 
---
 
# 📂 Project Structure
 
```text
Program.cs
│
├── Models
│   ├── Player.cs
│   ├── GameConfig.cs
│   └── GameType.cs
│
├── Services
│   ├── PlayerService.cs
│   ├── Validation.cs
│   ├── ScoreManager.cs
│   ├── RandomGenerator.cs
│   ├── AlphaNumeric.cs
│   └── GuessParser.cs
│
├── UI
│   ├── Menu.cs
│   ├── LeaderBoard.cs
│   └── ConsoleExtensions.cs
│
└── Config
    └── GameSettings.cs
 
