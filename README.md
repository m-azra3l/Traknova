# Traknova Telematics Service 

This project is a **controller-based Web API** built with **.NET 9** and **Visual Studio**, it provides endpoints for ingesting and managing telematics data, with support for crash detection events.

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/)
- SQL Server (local instance or containerized)
- Docker (optional, for containerized DB)

---

## Environment Variables

Sensitive configuration values are loaded from environment variables instead of `appsettings.json`.

- Create a `.env` file in the project root:
  ```env
  TELEMATICS_MssqlDB="your connection string"
  TELEMATICS_LogDirectory="your log directory"
  TELEMATICS_ASPNETCORE_ENVIRONMENT="Development"
  ```

- A sample `.env.sample.env` file is included for reference.

---

## Database Setup

### Local SQL Server

If you have SQL Server installed on your PC, you can connect directly using your connection string in `.env`.

### Containerized SQL Server (Optional)

If you prefer running SQL Server in Docker, there is a `docker-compose.yml` file is included in the solution directory.

---

## Migrations

Add migrations using either:

- **Package Manager Console (Visual Studio):**
  ```cmd
  Add-Migration InitialCreate -OutputDir Infrastructure/Migrations
  Update-Database
  ```

- **.NET CLI:**
  ```cmd
  dotnet ef migrations add InitialCreate --output-dir Infrastructure/Migrations
  dotnet ef database update
  ```

---

## Running the Web API

The Web API is containerized. You can run it with cli:

```cmd
dotnet run
```

Or build and run via `Visual Studio` options.

### Documentation Endpoints

- **Scalar docs** → `/docs`
- **Swagger UI** → `/documentations`

---

## Testing

- Unit tests use **xUnit** and EF Core’s **InMemory provider**.
- Sample payloads (`payload.json`) are included in the test project and copied to output for ingestion tests.

Run tests with cli using:

```cmd
dotnet test
```

Or use `TestExplorer` in `Visual Studio`.

---

## Summary

When starting a new project:
1. Set up `.env` with DB connection and log directory.
2. Spin up SQL Server (local or containerized).
3. Add and apply migrations.
4. Run the Web API and navigate to `/docs` or `/documentations`.
5. Run tests to validate repository ingestion logic.
```

---

## License

This project is licensed under the MIT License. See `LICENSE` for details.
