# Advanced Repository Pattern Implementation in .NET

This project demonstrates an advanced implementation of the Repository Pattern in .NET, using Entity Framework Core and SQLite. It provides a flexible and robust structure for data access, separating concerns and improving maintainability.

## 🇮🇹 Detailed Article (in Italian)

For a comprehensive guide on this implementation, please read our detailed article:
[Implementazione Avanzata del Repository Pattern in .NET](https://cosminirimescu.com/advanced-repository-pattern-dotnet)

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- An IDE of your choice (Visual Studio, VS Code, JetBrains Rider, etc.)

### Clone the Repository

```bash
git clone https://github.com/irimescucosmin/EF.RepositoryPattern.NET.git
cd EF.RepositoryPattern.NET
```

### Create the Initial Migration

Run the following command to create the initial database migration:
```bash
dotnet run --project EF.RepositoryPattern.NET
```

### Run the Application
```bash
dotnet ef migrations add --project EF.RepositoryPattern.NET/EF.RepositoryPattern.NET.csproj --startup-project EF.RepositoryPattern.NET/EF.RepositoryPattern.NET.csproj --context EF.RepositoryPattern.NET.Contexts.CustomersDbContext --configuration Release --verbose Initial --output-dir Migrations
```

The application will automatically apply the migration on startup.

## 🏗️ Project Structure
- `Interfaces/`: Contains interface definitions including `IBaseEntity`, `IBaseRepository<T>`, and `ICustomersRepository<T>`.
- `Repositories/`: Includes `BaseRepository<TEntity, TContext>` and `CustomersRepository<TEntity>` implementations.
- `Contexts/`: Contains CustomersDbContext for database configuration.
- `Controllers/`: Includes CustomersController demonstrating the usage of the repository.
- `Entities/`: Contains entity definitions.

## 🛠️ Key Features
- Generic base repository with CRUD operations
- Asynchronous methods for improved performance
- Separation of concerns with interfaces and concrete implementations
- Flexible design allowing easy extension for specific repositories

## 📚 Further Reading
- Entity Framework Core Documentation
- ASP.NET Core Documentation

## 🤝 Contributing
Contributions, issues, and feature requests are welcome! Feel free to check issues page.

## 📝 License
This project is MIT licensed.

---
Happy coding! 🚀👨‍💻👩‍💻