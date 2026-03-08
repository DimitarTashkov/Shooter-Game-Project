# 🎯 Shooter Game

A **grid-based tactical shooter** built with **C# 14**, **.NET 10**, and **Windows Forms**.  
Navigate a dynamically generated map, hunt enemies, and climb the leaderboard.

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-14.0-239120?logo=csharp)
![WinForms](https://img.shields.io/badge/UI-Windows%20Forms-0078D4)
![SQLite](https://img.shields.io/badge/DB-SQLite-003B57?logo=sqlite)
![License](https://img.shields.io/badge/License-MIT-yellow)

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Screenshots](#-screenshots)
- [Features](#-features)
- [Architecture](#-architecture)
- [Getting Started](#-getting-started)
- [How to Play](#-how-to-play)
- [Enemies](#-enemies)
- [Weapons](#-weapons)
- [Project Structure](#-project-structure)
- [Design Patterns](#-design-patterns)
- [Tech Stack](#-tech-stack)
- [Testing](#-testing)
- [Contributing](#-contributing)

---

## 🔭 Overview

Originally a console application, **Shooter Game** has been modernized into a full Windows Forms desktop game with GDI+ rendering, SQLite persistence, and clean OOP architecture. Players pick a weapon, enter a randomly generated grid map, and eliminate enemies by clicking cells or using keyboard controls. Scores are saved to a local SQLite database and displayed on a persistent leaderboard.

---

## 📸 Screenshots

> **Note:** The game uses a dark theme with GDI+ custom-rendered graphics. Below are text representations of each screen.

### Main Menu

```
┌──────────────────────────────────────┐
│                                      │
│          SHOOTER GAME                │
│      Console-to-WinForms Edition     │
│                                      │
│         ┌────────────────┐           │
│         │   START GAME   │           │
│         └────────────────┘           │
│         ┌────────────────┐           │
│         │  LEADERBOARD   │           │
│         └────────────────┘           │
│         ┌────────────────┐           │
│         │      EXIT      │           │
│         └────────────────┘           │
│                                      │
└──────────────────────────────────────┘
```

### Game Setup

```
┌───────────────────────────────┐
│                               │
│  Enter Username:              │
│  ┌───────────────────────┐    │
│  │ Player1               │    │
│  └───────────────────────┘    │
│                               │
│  Select Weapon:               │
│    ◉ Rifle   (20% headshot)   │
│    ○ Shotgun (33% headshot)   │
│    ○ Sniper  (10% headshot)   │
│                               │
│  ┌───────────────────────┐    │
│  │        BEGIN          │    │
│  └───────────────────────┘    │
└───────────────────────────────┘
```

### Gameplay

```
┌────────────────────────────────────────────────────────────┐
│                                                            │
│  ┌─────────────────────┐  ┌──────────────────────────────┐ │
│  │  ·  ·  ·  ·  ·  ·  │  │ Welcome, Player1!            │ │
│  │  ·  · (O) ·  ·  ·  │  │ Equipped weapon: Rifle       │ │
│  │  ·  ·  ·  · (T) ·  │  │ 10 enemies generated on map  │ │
│  │  · [+] ·  ·  ·  ·  │  │ Click a cell or use WASD     │ │
│  │  ·  ·  · (W) ·  ·  │  │ > Orc was shot for 440 dmg   │ │
│  │  ·  ·  ·  ·  · (Z) │  │ > Orc has regenerated 180    │ │
│  └─────────────────────┘  │ > No enemy at [2,1]          │ │
│                            └──────────────────────────────┘ │
│  Weapon: Rifle             Kills: 3 | Dmg: 1820 | Pts: 1507│
│                            Enemies remaining: 7             │
│                            ┌───────────┐  ┌──────────────┐  │
│                            │  HINT (H) │  │  END GAME    │  │
│                            └───────────┘  └──────────────┘  │
└────────────────────────────────────────────────────────────┘

Legend:  (O) Orc  (T) Tank  (W) Warrior  (Z) Wizard  [+] Cursor
```

### Leaderboard

```
┌─────────────────────────────────────┐
│                                     │
│         🏆 LEADERBOARD              │
│                                     │
│  ┌─────┬───────────┬───────┬──────┐ │
│  │Rank │ Username  │ Score │ Date │ │
│  ├─────┼───────────┼───────┼──────┤ │
│  │  1  │ Player1   │ 4520  │ 03-08│ │
│  │  2  │ ProGamer  │ 3200  │ 03-07│ │
│  │  3  │ Newbie    │ 1100  │ 03-06│ │
│  └─────┴───────────┴───────┴──────┘ │
│                                     │
│           ┌──────────┐              │
│           │  CLOSE   │              │
│           └──────────┘              │
└─────────────────────────────────────┘
```

---

## ✨ Features

| Feature | Description |
|---|---|
| **Dynamic Maps** | Grid dimensions are randomized each game (2×2 up to 9×9) |
| **GDI+ Rendering** | Smooth anti-aliased graphics with double buffering for flicker-free gameplay |
| **4 Enemy Types** | Orc, Tank, Warrior, Wizard — each with unique HP, size, and regeneration |
| **3 Weapons** | Rifle, Shotgun, Sniper — each with different damage and headshot chances |
| **Hint System** | Press **H** to get directional hints toward the nearest enemy |
| **Live Stats** | Real-time kill count, damage, and point tracking via Observer pattern |
| **Leaderboard** | Top 10 scores persisted in SQLite with alternating-row styling |
| **Keyboard + Mouse** | Full WASD/Arrow + Space controls, or point-and-click on the grid |

---

## 🏗 Architecture

```
                    ┌──────────┐
                    │ StartUp  │
                    └────┬─────┘
                         │
                 ┌───────▼───────┐
                 │ MainMenuForm  │
                 └───┬───────┬───┘
                     │       │
            ┌────────▼──┐  ┌─▼──────────────┐
            │ SetupForm │  │ LeaderboardForm │
            └────┬──────┘  └────────────────┘
                 │
            ┌────▼─────┐
            │ GameForm  │──────► GDI+ Rendering
            └────┬──────┘
                 │
            ┌────▼──────┐
            │ Controller │
            └──┬───┬──┬─┘
               │   │  │
    ┌──────────▼┐ ┌▼──▼──────────┐
    │Repositories│ │ EnemyFactory │
    │ (Users,   │ └──────────────┘
    │  Enemies, │
    │  Weapons, │
    │  Maps)    │
    └───────────┘
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- Windows (Windows Forms requires the Windows desktop runtime)
- Visual Studio 2022/2026+ (recommended) or any .NET-compatible IDE

### Build & Run

```bash
# Clone the repository
git clone https://github.com/DimitarTashkov/Shooter-Game-Project.git
cd Shooter-Game-Project

# Restore and build
dotnet build

# Run the game
dotnet run --project Shooter_Game0.1
```

The SQLite database (`shootergame.db`) is created automatically on first use — no manual setup needed.

### Run Tests

```bash
dotnet test
```

---

## 🎮 How to Play

1. **Launch** the game and click **START GAME**
2. **Enter** your username and **select a weapon**
3. **Navigate** the grid:

   | Input | Action |
   |---|---|
   | `W` / `↑` | Move cursor up |
   | `S` / `↓` | Move cursor down |
   | `A` / `←` | Move cursor left |
   | `D` / `→` | Move cursor right |
   | `Space` / `Enter` | Shoot at cursor position |
   | `H` | Get a hint (nearest enemy direction) |
   | `R` | Show current stats |
   | **Mouse click** | Shoot at clicked cell |

4. **Eliminate all enemies** to complete the round
5. Click **END GAME** to save your score and see the final report

### Scoring

```
Points = (Enemies Killed × 300) + (Total Damage Dealt ÷ 3)
```

---

## 👾 Enemies

| Enemy | Size | Base HP | Life (Size × HP) | Regen (% of Life) | Color |
|---|---|---|---|---|---|
| **Orc** | 15 | 40 | 600 | 30% (180) | 🟢 Green |
| **Wizard** | 25 | 50 | 1,250 | 20% (250) | 🟣 Purple |
| **Warrior** | 30 | 30 | 900 | 10% (90) | 🔴 Crimson |
| **Tank** | 50 | 80 | 4,000 | 40% (1,600) | ⚪ Gray |

- Each enemy **regenerates health once** after the first hit
- Enemies **relocate** to a new random cell after surviving a shot
- Rendered as colored circles with a letter initial on the map

---

## 🔫 Weapons

| Weapon | Ammo | Power | Damage Formula | Headshot Chance |
|---|---|---|---|---|
| **Rifle** | 22 | 20 | `Ammo × Power` = 440 | 20% (1 in 5) |
| **Shotgun** | 9 | 33 | `Ammo × Power` = 297 | 33% (1 in 3) |
| **Sniper** | 1 | 800 | `Ammo × Power` = 800 | 10% (1 in 10) |

- On a **headshot**, damage is multiplied (bonus hit)
- Weapon is locked for the entire game session based on your setup choice

---

## 📁 Project Structure

```
Shooter-Game-Project/
├── Shooter_Game0.1/                    # Main game project
│   ├── Core/
│   │   ├── Controller.cs               # Central game logic & orchestration
│   │   ├── DataBuilder.cs              # Builder pattern: creates game objects
│   │   ├── Engine.cs                   # Legacy console engine
│   │   └── Contracts/                  # IController, IEngine, IDataBuilder
│   ├── Data/
│   │   ├── ShooterGameContext.cs       # EF Core DbContext (SQLite)
│   │   └── PlayerScore.cs             # Leaderboard entity
│   ├── Factories/
│   │   └── EnemyFactory.cs            # Factory Method pattern
│   ├── Forms/
│   │   ├── MainMenuForm.cs/.Designer.cs
│   │   ├── SetupForm.cs/.Designer.cs
│   │   ├── GameForm.cs/.Designer.cs    # GDI+ rendering, input handling
│   │   └── LeaderboardForm.cs/.Designer.cs
│   ├── Models/
│   │   ├── Enemies/
│   │   │   ├── Contracts/IEnemy.cs
│   │   │   └── Models/                 # Orc, Tank, Warrior, Wizard
│   │   ├── Maps/
│   │   │   ├── Contracts/IMap.cs
│   │   │   └── Map.cs, DefaultMap.cs, CustomMap.cs
│   │   ├── Users/
│   │   │   ├── Contracts/IUser.cs      # Observer pattern events
│   │   │   └── User.cs
│   │   └── Weapons/
│   │       ├── Contracts/IWeapon.cs
│   │       └── Models/                 # Rifle, Shotgun, Sniper
│   ├── Repositories/                   # In-memory collections
│   │   ├── EnemiesRepository.cs
│   │   ├── EnemiesCoordinatesRepository.cs
│   │   ├── WeaponsRepository.cs
│   │   ├── MapsRepository.cs
│   │   └── UsersRepository.cs
│   ├── Utilities/
│   │   ├── Hinter/Hinter.cs           # Directional hint algorithm
│   │   ├── Messages/                   # Output & exception message constants
│   │   └── Randomizer/Randomizer.cs   # Map, enemy, weapon randomization
│   ├── IO/                             # Legacy console reader/writer
│   ├── Migrations/                     # EF Core SQLite migrations
│   └── StartUp.cs                      # Application entry point
├── Shooter_Game0.1-Tests/              # NUnit test project
│   └── UnitTest1.cs
└── README.md
```

---

## 🧩 Design Patterns

| Pattern | Where | Purpose |
|---|---|---|
| **Factory Method** | `EnemyFactory` | Centralizes enemy creation, eliminates scattered `new` calls |
| **Builder** | `DataBuilder` | Constructs enemies, weapons, maps, and users from string identifiers |
| **Observer** | `IUser.StatsChanged` | Real-time UI stat updates when kills/damage change |
| **Repository** | `*Repository` classes | In-memory collections with add/remove/query abstraction |
| **MVC-like** | Forms ↔ Controller | Forms handle display; `Controller` owns all game logic |

---

## 🛠 Tech Stack

| Layer | Technology |
|---|---|
| **Language** | C# 14.0 |
| **Runtime** | .NET 10 (Windows Desktop) |
| **UI** | Windows Forms + GDI+ custom painting |
| **Database** | SQLite via Entity Framework Core 10 |
| **Testing** | NUnit 3 + NUnit3TestAdapter |
| **IDE** | Visual Studio 2026 |

---

## 🧪 Testing

The project includes an **NUnit** test suite in `Shooter_Game0.1-Tests/`.

```bash
# Run all tests
dotnet test

# Run from Visual Studio
# Test Explorer → Run All
```

---

## 🤝 Contributing

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/my-feature`)
3. **Commit** your changes (`git commit -m "Add my feature"`)
4. **Push** to the branch (`git push origin feature/my-feature`)
5. **Open** a Pull Request

---

## 👤 Author

**Dimitar Tashkov** — [GitHub](https://github.com/DimitarTashkov)

---

<p align="center">
  Made with ❤️ in C# &nbsp;|&nbsp; ⭐ Star the repo if you enjoyed the game!
</p>
