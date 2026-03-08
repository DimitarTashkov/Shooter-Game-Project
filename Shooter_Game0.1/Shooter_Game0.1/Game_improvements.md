# Shooter Game: Advanced Features & Polish Plan

## 1. Context
We have successfully migrated the "Shooter Game" from a Console Application to a Windows Forms application. The core OOP logic (`Models`, `Repositories`, `Controller`) is intact. 

## 2. Goal
I want to upgrade this game for my university OOP course project. We need to implement proper Gang of Four (GoF) design patterns, integrate a local database for a Leaderboard, and polish the WinForms UI with dynamic resizing and smooth rendering.

Please act as a Senior .NET WinForms Expert and implement the following phases one at a time when I ask for them.

---

## 3. Execution Phases

### Phase 1: Database Integration (The Leaderboard)
* **Tech Stack:** Use **Entity Framework Core** with **SQLite** (`Microsoft.EntityFrameworkCore.Sqlite`). This keeps the database local and lightweight.
* **Models:** Create a `PlayerScore` entity (Id, Username, Score, DateAchieved).
* **DbContext:** Create a `ShooterGameContext` to manage the SQLite connection. Ensure it auto-creates the database on startup (`Database.EnsureCreated()`).
* **UI:** Create a `LeaderboardForm` that reads from the database and displays the top 10 scores in a `DataGridView`. Add a "View Leaderboard" button to the `MainMenuForm`.

### Phase 2: Design Patterns Application (Crucial for OOP Grade)
1. **Factory Method Pattern:** * Remove direct instantiations of enemies (e.g., `new Orc()`). 
   * Create an `EnemyFactory` class with a method `IEnemy CreateEnemy(string type, int x, int y)`. Use this inside the `Controller` to spawn enemies dynamically.
2. **Observer Pattern (Events):**
   * Decouple the UI from the Models. Add a `HealthChanged` event to the `IUser` interface and `User` class.
   * Make the `GameForm` subscribe to this event. When the player takes damage, the event fires, and the `GameForm` updates the Health Bar UI automatically.

### Phase 3: Dynamic Form Resizing 
Instead of hardcoding the rendering coordinates to a specific screen size (like 800x600), we need the game to support window resizing natively.
* **Logic:** Update the rendering logic in `GameForm.OnPaint`. Calculate the *relative* position of entities based on a base resolution, and scale them according to the actual `ClientSize.Width` and `ClientSize.Height` of the current Form.
* Example: If the player is at `X=400` on an `800` wide map, their relative X is `0.5f`. If the window is resized to `1920` wide, render the player at `1920 * 0.5f` (`960`).

### Phase 4: Gameplay Polish & Rendering
1. **Smooth Graphics:** Ensure the `GameForm` has Double Buffering enabled in its constructor to completely eliminate screen flickering:
   ```csharp
   this.DoubleBuffered = true;
   this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);