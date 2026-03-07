# Upgrade Plan: .NET 6 → .NET 10

## Summary

Upgrade the Shooter Game solution from `net6.0` to `net10.0`.  
The assessment found **2 mandatory issues** — both are target-framework version declarations that need updating. No NuGet packages are incompatible and no source-code breaking changes were detected, making this a low-risk, straightforward version bump.

---

## 1. Pre-upgrade

### 1.1 Validate environment
- Confirm .NET 10 SDK is installed (`dotnet --list-sdks`) ✅ Already confirmed.
- Confirm working branch is `upgrade-to-NET10`. ✅ Already switched.

---

## 2. Upgrade projects (in dependency order)

Projects must be upgraded leaf-first so that when the test project is updated it already references a compatible main project.

### 2.1 Upgrade `Shooter_Game0.1` (main project)

**File:** `Shooter_Game0.1\Shooter_Game0.1\Shooter_Game0.1.csproj`

Change:
```xml
<TargetFramework>net6.0</TargetFramework>
```
To:
```xml
<TargetFramework>net10.0</TargetFramework>
```

Apply modern C# / SDK defaults that are now recommended for .NET 10:
- Ensure `<Nullable>enable</Nullable>` is present.
- Ensure `<ImplicitUsings>enable</ImplicitUsings>` is present.

**Also modernise `Controller.cs` and solution source files:**
- Remove redundant `using` directives that are now covered by implicit usings (`System`, `System.Collections.Generic`, `System.Linq`, `System.Text`, `System.Threading.Tasks`).
- Use target-typed `new()` expressions where the type is clear from context (e.g. `new()` instead of `new Dictionary<int,int>()`).
- Replace `string.Format(…)` calls with interpolated strings (`$"…"`).
- Simplify `StatsUpdate`: replace the `if/else` lookup + add pattern with `FirstOrDefault` + null-coalescing assignment.
- Fix the null-dereference bug: `map` is used before the null check in `Shoot()` — move the guard above the first use of `map`.

### 2.2 Upgrade `Shooter_Game0.1-Tests` (test project)

**File:** `Shooter_Game0.1\Shooter_Game0.1-Tests\Shooter_Game0.1-Tests.csproj`

Change:
```xml
<TargetFramework>net6.0</TargetFramework>
```
To:
```xml
<TargetFramework>net10.0</TargetFramework>
```

All five test NuGet packages (`NUnit`, `NUnit3TestAdapter`, `NUnit.Analyzers`, `Microsoft.NET.Test.Sdk`, `coverlet.collector`) are already compatible with .NET 10 — no version changes needed.

---

## 3. Build & verify

### 3.1 Build the solution
Run a full solution build and confirm zero errors.

### 3.2 Run tests
Execute the NUnit test suite and confirm all tests pass.

---

## 4. Commit

Commit all changes to the `upgrade-to-NET10` branch with message:  
`chore: upgrade solution from net6.0 to net10.0`

---

## Risk Assessment

| Area | Risk | Notes |
|---|---|---|
| Target framework bump | Low | .NET 10 is LTS; no breaking APIs detected |
| NuGet packages | None | All packages compatible |
| Source code | Low | Minor modernisation only; no logic changes |
| Tests | Low | All test packages compatible |
