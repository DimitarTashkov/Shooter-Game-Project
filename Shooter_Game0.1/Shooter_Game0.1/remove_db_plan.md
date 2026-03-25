# Plan: Refactor Leaderboard to use JSON Serialization instead of SQLite Database

## Context
The project is a Windows Forms desktop game. Currently, it uses Entity Framework Core and SQLite to save player scores (Leaderboard) via `UsersRepository`. 
To make the application fully portable and standalone (and to fulfill a specific academic requirement for "File Serialization"), we need to completely remove the SQLite database and replace it with a simple JSON file serialization mechanism.

## Objectives
Remove all Entity Framework Core dependencies and refactor `UsersRepository.cs` to serialize and deserialize `User` objects to a `leaderboard.json` file.

## Task 1: Rewrite UsersRepository.cs
**File to modify:** `Repositories/UsersRepository.cs`
- Remove any database context injection or usage.
- The repository should hold a `List<IUser>` in memory.
- Implement a private `SaveUsers()` method using `System.Text.Json.JsonSerializer` to write the list of users to `leaderboard.json`. Note: You will need to cast `IUser` to the concrete `User` class before serializing.
- Implement a private `LoadUsers()` method that reads from `leaderboard.json` (if it exists) and deserializes it back into a list.
- Update `AddModel()` and `RemoveModel()` to call `SaveUsers()` after modifying the collection.

## Task 2: Clean up references in UI and Startup
**Files to check/modify:** `StartUp.cs`, `Forms/LeaderboardForm.cs`, and any other file initializing the DB context.
- Remove all `using Shooter_Game0._1.Data;` directives.
- Remove any instantiation of `ShooterGameContext` (e.g., `using (var context = new ShooterGameContext()) { context.Database.EnsureCreated(); }` in `StartUp.cs`).
- Ensure `LeaderboardForm` simply calls the new `UsersRepository` to get the list of users and bind them to the `DataGridView` without relying on EF Core.

## CRITICAL INSTRUCTIONS FOR CLAUDE:
1. Output the completely rewritten `Repositories/UsersRepository.cs`.
2. Output the cleaned-up `StartUp.cs` and `Forms/LeaderboardForm.cs` (only output the relevant methods/constructors if the files are too big, but ensure all DB context code is removed).
3. Do not use abstract placeholders. Write the actual functional C# code.
4. Add the exact file path as a comment at the top of each code block.