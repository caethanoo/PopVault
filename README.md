# PopVault 🎵

PopVault is a **Music Collection Manager** aiming to evolve into an AI-Powered Virtual Music Critic.

## Current Status: Week 2 (Object-Oriented Programming)

The project is structured following **Clean Architecture** principles:
- **`src/PopVault.Domain`**: Contains the core logic (`Artist`, `Album`, `Review`).
- **`src/PopVault.ConsoleApp`**: The CLI interface for managing the collection.

## How to Run

Requirements: .NET 8 SDK.

1. Clone the repository.
2. Navigate to the root folder.
3. Run the application:
   ```bash
   dotnet run --project backend/src/PopVault.ConsoleApp/PopVault.ConsoleApp.csproj
   ```

## Features
- **Register**: Add Artists and their Albums to the collection.
- **Review**: Rate albums (0-10) and leave comments.
- **Statistics**: View the best-rated album and collection average score.
- **Architecture**: Domain-Driven Design with rich entities.

## Roadmap
- [x] Week 1: Logic Fundamentals (Loops, Lists)
- [x] Week 2: POO & Domain Modeling
- [ ] Week 3: Git & Version Control
- [ ] Week 4: Databases (EF Core)
- [ ] Week 5: Clean Architecture API
- [ ] Week 6: AI Integration (Semantic Kernel)
