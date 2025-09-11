# EncantoWebAPI (.NET 8 Web API)

A .NET 8 Web API project that uses MongoDB as the primary data store. It provides endpoints for:

- User authentication (signup / login)
- Session management (custom session key with expiration)
- User profile management (profile, occupation, address)
- Event management (organizer + participants model)
- Utility endpoints

## Technology Stack

- .NET 8 (C# 12)
- ASP.NET Core Minimal / Controllers
- MongoDB.Driver

## Project Structure (high level)

```
Accessors/        # Data access layers (Mongo collections wrappers)
Managers/         # Business logic / orchestration
Controllers/      # API endpoints
Middlewares/      # Custom middlewares (e.g., session validation)
Models/           # POCO data contracts / Mongo entities / DTOs
appsettings.json  # Base configuration (non?secret)
```

## Configuration

Application configuration lives in `appsettings.json`. Do NOT store secrets there in production.

Example (redacted):
```json
{
  "MongoDBSettings": {
    "ConnectionURI": "mongodb+srv://<user>:<password>@cluster/",
    "DatabaseName": "YourDbName",
    "UsersCollectionName": "Users",
    "AddressCollectionName": "Addresses",
    "OccupationDetailsCollectionName": "OccupationDetails",
    "LoginCredentialsCollectionName": "LoginCredentials",
    "SessionsCollectionName": "Sessions",
    "EventsCollectionName": "Events"
  }
}
```

For local secret handling prefer:
- dotnet user-secrets (during development)
- Environment variables / Kubernetes secrets / Azure Key Vault in production

## Running the Project

```bash
dotnet restore
dotnet build
dotnet run
```
The API will start (by default) on: `https://localhost:7183` / `http://localhost:5183` (ports may vary).

## MongoDB Setup

1. Ensure a MongoDB instance (Atlas cluster or local) is reachable.
2. Create the database named in `DatabaseName`.
3. Collections are created automatically on first insert.

## Session Flow (Simplified)

1. User signs up (credentials + profile stored).
2. User logs in -> session document created with `SessionKey` + expiration.
3. Middleware validates `SessionKey` on protected endpoints.
4. Expired / invalid keys rejected.

## Development Guidelines

- Keep access logic in Accessors; no business rules there.
- Put validation / orchestration in Managers.
- Controllers should stay thin.
- Add unit tests for managers (if test project added later).

## Extending

Suggested next improvements:
- Add logging (Serilog)
- Add FluentValidation for request DTOs
- Add refresh token / revoke session support
- Add pagination & filtering for events
- Add OpenAPI/Swagger annotations for better docs

## Build & Publish

Release build:
```bash
dotnet publish -c Release -o out
```
Run published output:
```bash
dotnet out/EncantoWebAPI.dll
```

## .gitignore

A tailored `.gitignore` is included to exclude build artifacts, user files, secrets, and data dumps.

## License

Add a license file (e.g., MIT) if this will be public.

---
