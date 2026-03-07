# Shooter Game .NET 10 Upgrade Tasks

## Overview

This document tracks the execution of upgrading the Shooter Game solution from .NET 6.0 to .NET 10.0. Both projects will be upgraded together, followed by testing and final commit.

**Progress**: 3/4 tasks complete (75%) ![0%](https://progress-bar.xyz/75)

---

## Tasks

### [✓] TASK-001: Verify prerequisites *(Completed: 2026-03-07 15:52)*
**References**: Plan §1.1

- [✓] (1) Verify .NET 10 SDK is installed (`dotnet --list-sdks`)
- [✓] (2) .NET 10 SDK version is available (**Verify**)

---

### [✓] TASK-002: Upgrade both projects to .NET 10 *(Completed: 2026-03-07 15:54)*
**References**: Plan §2.1, Plan §2.2

- [✓] (1) Update `Shooter_Game0.1\Shooter_Game0.1\Shooter_Game0.1.csproj`: change `<TargetFramework>net6.0</TargetFramework>` to `<TargetFramework>net10.0</TargetFramework>`
- [✓] (2) Add `<Nullable>enable</Nullable>` and `<ImplicitUsings>enable</ImplicitUsings>` to main project if not present
- [✓] (3) Update `Shooter_Game0.1\Shooter_Game0.1-Tests\Shooter_Game0.1-Tests.csproj`: change `<TargetFramework>net6.0</TargetFramework>` to `<TargetFramework>net10.0</TargetFramework>`
- [✓] (4) Modernise `Controller.cs` per Plan §2.1: remove redundant usings, use target-typed `new()`, replace `string.Format` with interpolated strings, simplify `StatsUpdate`, fix null-dereference in `Shoot()`
- [✓] (5) Restore all dependencies
- [✓] (6) All dependencies restored successfully (**Verify**)
- [✓] (7) Build solution and fix any compilation errors
- [✓] (8) Solution builds with 0 errors (**Verify**)

---

### [✓] TASK-003: Run test suite and validate upgrade *(Completed: 2026-03-07 17:55)*
**References**: Plan §3.2

- [✓] (1) Run NUnit tests in Shooter_Game0.1-Tests project
- [✓] (2) Fix any test failures
- [✓] (3) Re-run tests after fixes
- [✓] (4) All tests pass with 0 failures (**Verify**)

---

### [▶] TASK-004: Final commit
**References**: Plan §4

- [▶] (1) Commit all changes with message: "chore: upgrade solution from net6.0 to net10.0"

---







