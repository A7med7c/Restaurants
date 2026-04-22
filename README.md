# Restaurants API

A layered ASP.NET Core Web API for managing restaurants, dishes, orders, and users. The solution uses Clean Architecture-style separation (API, Application, Domain, Infrastructure) with EF Core, MediatR, FluentValidation, and ASP.NET Core Identity.

## Features
- Restaurants CRUD with paging, sorting, and filtering
- Dishes CRUD scoped to a restaurant
- Order creation and lookup
- Role-based authorization and custom authorization policies
- ASP.NET Core Identity endpoints for authentication and user management
- Swagger/OpenAPI documentation in Development
- Serilog request logging

## Tech Stack
- **.NET 9 / ASP.NET Core**
- **Entity Framework Core** (SQL Server)
- **ASP.NET Core Identity**
- **MediatR**, **AutoMapper**, **FluentValidation**
- **Serilog**
- **Swagger / OpenAPI**

## Project Structure
- `Restaurants.Api` — API host, controllers, middleware, and configuration
- `Restaurants.Application` — application logic, CQRS handlers, validators, DTOs
- `Restaurants.Dpomain` — domain entities, constants, and interfaces
- `Restaurants.Infrastructure` — EF Core context, repositories, seeding, authorization services

## Getting Started

### Prerequisites
- .NET SDK 9
- SQL Server (local or remote)

### Configuration
Update the connection string in `Restaurants.Api/appsettings.Development.json`:

```json
"ConnectionStrings": {
  "RestaurantsDb": "Server=...;Database=RestaurantsDb;Trusted_Connection=True;Encrypt=False;"
}
```

### Database Migrations
Apply migrations using the Infrastructure project with the API as the startup project:

```bash
dotnet ef database update --project Restaurants.Infrastructure --startup-project Restaurants.Api
```

### Run the API

```bash
dotnet run --project Restaurants.Api
```

Default local URLs (from `launchSettings.json`):
- https://localhost:7117
- http://localhost:5215

Swagger UI is available in Development at:
- https://localhost:7117/swagger

### Seed Data
On startup, the API seeds:
- Default roles (`User`, `Admin`, `Owner`, `Driver`)
- Sample restaurants and dishes

## API Overview
Key endpoints (see Swagger for the full list):
- `GET /api/restaurants`
- `GET /api/restaurants/{id}`
- `POST /api/restaurants`
- `PATCH /api/restaurants/{id}`
- `DELETE /api/restaurants/{id}`
- `GET /api/restaurants/{restaurantId}/dishes`
- `POST /api/restaurants/{restaurantId}/dishes`
- `PUT /api/restaurants/{restaurantId}/dishes/{dishId}`
- `DELETE /api/restaurants/{restaurantId}/dishes/{dishId}`
- `GET /api/orders/{id}`
- `POST /api/orders`
- Identity endpoints under `/api/identity`

## Build and Test

```bash
dotnet build
dotnet test
```

## License
This project does not currently include a license file.
