# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v10.0.

## Table of Contents

- [Executive Summary](#executive-Summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
- [Aggregate NuGet packages details](#aggregate-nuget-packages-details)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Projects Relationship Graph](#projects-relationship-graph)
- [Project Details](#project-details)

  - [Shooter_Game0.1\Shooter_Game0.1.csproj](#shooter_game01shooter_game01csproj)
  - [Shooter_Game0.1-Tests\Shooter_Game0.1-Tests.csproj](#shooter_game01-testsshooter_game01-testscsproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 2 | All require upgrade |
| Total NuGet Packages | 5 | All compatible |
| Total Code Files | 41 |  |
| Total Code Files with Incidents | 2 |  |
| Total Lines of Code | 2038 |  |
| Total Number of Issues | 2 |  |
| Estimated LOC to modify | 0+ | at least 0,0% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| [Shooter_Game0.1\Shooter_Game0.1.csproj](#shooter_game01shooter_game01csproj) | net6.0 | 🟢 Low | 0 | 0 |  | DotNetCoreApp, Sdk Style = True |
| [Shooter_Game0.1-Tests\Shooter_Game0.1-Tests.csproj](#shooter_game01-testsshooter_game01-testscsproj) | net6.0 | 🟢 Low | 0 | 0 |  | ClassLibrary, Sdk Style = True |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 5 | 100,0% |
| ⚠️ Incompatible | 0 | 0,0% |
| 🔄 Upgrade Recommended | 0 | 0,0% |
| ***Total NuGet Packages*** | ***5*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 1804 |  |
| ***Total APIs Analyzed*** | ***1804*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| coverlet.collector | 3.1.2 |  | [Shooter_Game0.1-Tests.csproj](#shooter_game01-testsshooter_game01-testscsproj) | ✅Compatible |
| Microsoft.NET.Test.Sdk | 17.3.2 |  | [Shooter_Game0.1-Tests.csproj](#shooter_game01-testsshooter_game01-testscsproj) | ✅Compatible |
| NUnit | 3.13.3 |  | [Shooter_Game0.1-Tests.csproj](#shooter_game01-testsshooter_game01-testscsproj) | ✅Compatible |
| NUnit.Analyzers | 3.5.0 |  | [Shooter_Game0.1-Tests.csproj](#shooter_game01-testsshooter_game01-testscsproj) | ✅Compatible |
| NUnit3TestAdapter | 4.3.0 |  | [Shooter_Game0.1-Tests.csproj](#shooter_game01-testsshooter_game01-testscsproj) | ✅Compatible |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>📦&nbsp;Shooter_Game0.1.csproj</b><br/><small>net6.0</small>"]
    P2["<b>📦&nbsp;Shooter_Game0.1-Tests.csproj</b><br/><small>net6.0</small>"]
    P2 --> P1
    click P1 "#shooter_game01shooter_game01csproj"
    click P2 "#shooter_game01-testsshooter_game01-testscsproj"

```

## Project Details

<a id="shooter_game01shooter_game01csproj"></a>
### Shooter_Game0.1\Shooter_Game0.1.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** DotNetCoreApp
- **Dependencies**: 0
- **Dependants**: 1
- **Number of Files**: 39
- **Number of Files with Incidents**: 1
- **Lines of Code**: 1721
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P2["<b>📦&nbsp;Shooter_Game0.1-Tests.csproj</b><br/><small>net6.0</small>"]
        click P2 "#shooter_game01-testsshooter_game01-testscsproj"
    end
    subgraph current["Shooter_Game0.1.csproj"]
        MAIN["<b>📦&nbsp;Shooter_Game0.1.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#shooter_game01shooter_game01csproj"
    end
    P2 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 1385 |  |
| ***Total APIs Analyzed*** | ***1385*** |  |

<a id="shooter_game01-testsshooter_game01-testscsproj"></a>
### Shooter_Game0.1-Tests\Shooter_Game0.1-Tests.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 1
- **Dependants**: 0
- **Number of Files**: 2
- **Number of Files with Incidents**: 1
- **Lines of Code**: 317
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["Shooter_Game0.1-Tests.csproj"]
        MAIN["<b>📦&nbsp;Shooter_Game0.1-Tests.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#shooter_game01-testsshooter_game01-testscsproj"
    end
    subgraph downstream["Dependencies (1"]
        P1["<b>📦&nbsp;Shooter_Game0.1.csproj</b><br/><small>net6.0</small>"]
        click P1 "#shooter_game01shooter_game01csproj"
    end
    MAIN --> P1

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 419 |  |
| ***Total APIs Analyzed*** | ***419*** |  |

