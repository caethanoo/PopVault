# PopVault

PopVault is a .NET 8 solution built following **Clean Architecture** principles. It manages music albums with an AI-enhanced description feature.

## 🏗️ Architecture

The solution is divided into 4 layers:

1.  **PopVault.Domain** (Core): Contains Enterprise Logic.
    *   Entities (`Album`)
    *   Interfaces (`IAlbumRepository`)
    *   *No external dependencies.*

2.  **PopVault.Application** (Business Logic): Orchestration and Use Cases.
    *   DTOs (`AlbumDto`, `CreateAlbumDto`)
    *   Services (`AlbumService`, `IAlbumService`)
    *   *Depends on Domain.*

3.  **PopVault.Infrastructure** (External concerns): Implementation of interfaces.
    *   Data Access with **Entity Framework Core** (InMemory).
    *   Repositories (`AlbumRepository`).
    *   *Depends on Domain and Application.*

4.  **PopVault.API** (Presentation): Entry point.
    *   REST API Controllers.
    *   Dependency Injection configuration.
    *   *Depends on Application and Infrastructure.*

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK

### How to Run
1.  **Clone the repository:**
    ```bash
    git clone https://github.com/caethanoo/PopVault.git
    cd PopVault
    ```

2.  **Build the Solution:**
    ```bash
    dotnet build
    ```

3.  **Run the API:**
    ```bash
    dotnet run --project src/PopVault.API
    ```

## 🛠️ Technologies
- **.NET 8**
- **Entity Framework Core (InMemory)** for rapid prototyping.
- **Dependency Injection** (Built-in container).
