# ABP Cinema API

A .NET 9 ASP.NET Core Web API for managing cinema operations, built with a layered architecture pattern.

- **ABPCinemaAPI.Api** - ASP.NET Core Web API entry point with Swagger/OpenAPI documentation
- **ABPCinemaAPI.BLL** - Business Logic Layer containing domain services and business rules
- **ABPCinemaAPI.DAL** - Data Access Layer managing data persistence and repository patterns

## Technology Stack

- **.NET 9** - Latest .NET framework
- **ASP.NET Core** - Web API framework
- **Swagger/OpenAPI** - API documentation and testing
- **C#** - Primary programming language

## Prerequisites

- .NET 9 SDK or later
- Visual Studio 2022 or compatible IDE
- Git

## Getting Started

### Clone the Repository

```
git clone https://github.com/BlackkTurtle/ABPCinemaAPI.git
cd ABPCinemaAPI
```

### Build the Solution

```
dotnet build
```

### Run the API

```
cd ABPCinemaAPI.Api
dotnet run
```

The API will be available at `https://localhost:5001` (or the configured port).

### Access Swagger UI

Once the application is running, navigate to:
```
https://localhost:5001/swagger
```

## Project Structure

```
ABPCinemaAPI/
├── ABPCinemaAPI.Api/          # Web API project
│   ├── Program.cs             # Application startup configuration
│   └── Controllers/           # API endpoints
├── ABPCinemaAPI.BLL/          # Business Logic Layer
│   └── Properties/
│       └── AssemblyInfo.cs
├── ABPCinemaAPI.DAL/          # Data Access Layer
│   └── Properties/
│       └── AssemblyInfo.cs
└── README.md                  # This file
```

## Configuration

The application is configured in `Program.cs` and can be customized for:
- Swagger/OpenAPI generation
- HTTPS redirection
- Global exception handler middleware
- Specification and Selector Patterns
- Generic Repository Pattern
- UnitOfwork

## Development

### Building

```
dotnet build
```

### Running Tests

```
dotnet test
```
