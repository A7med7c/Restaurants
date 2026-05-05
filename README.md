# Restaurants API

A layered ASP.NET Core Web API for restaurant management and ordering. The solution follows Clean Architecture separation (API, Application, Domain, Infrastructure) and uses EF Core, MediatR, FluentValidation, and ASP.NET Core Identity.

## Features

- Restaurants CRUD with paging, sorting, and filtering
- Dishes CRUD scoped to a restaurant
- Cart management with checkout flow
- Order creation, status tracking, and lookup
- Paymob payment integration and callback handling
- Role-based authorization with custom policies
- ASP.NET Core Identity endpoints for authentication and user management
- Seed data for roles, restaurants, and dishes
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
- `Restaurants.Domain` — domain entities, constants, and interfaces
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

## API Documentation

This documentation is generated from the controllers, commands, DTOs, validators, and enums in this repository.

### Authentication & Headers

- JWT Bearer is required on endpoints marked with `Authorize` or role constraints.
- `Content-Type: application/json` for requests with a body.
- `Authorization: Bearer {token}` for protected endpoints.

Roles:

- `Admin`, `Owner`, `User`, `Driver`

### Enums

PaymentMethod:

- `Cash`
- `Paymob`

PaymentStatus:

- `Pending`
- `Paid`
- `Failed`

OrderStatus:

- `Pending`
- `Confirmed`
- `Preparing`
- `Ready`
- `OnTheWay`
- `Delivered`
- `Cancelled`

SortDirection:

- `Ascending`
- `Descending`

### Restaurants

#### GET /api/restaurants

Description:
Returns a paged list of restaurants.

Headers:
None required.

Query:

- `searchPhrase`: string | null
- `pageNumber`: number
- `pageSize`: number
- `sortDirection`: `Ascending` | `Descending`
- `sortBy`: string | null

Response (200):

```json
{
  "items": [
    {
      "id": 1,
      "name": "Gourmet Delights",
      "description": "A culinary experience",
      "category": "American",
      "hasDelivery": true,
      "city": "Metropolis",
      "street": "456 Gourmet Avenue",
      "postalCode": "54-551",
      "dishes": [
        {
          "id": 10,
          "name": "Burger",
          "description": "Juicy burger",
          "price": 8.5,
          "kiloCalories": 650,
          "restaurantId": 1
        }
      ]
    }
  ],
  "totalPages": 1,
  "totalItemsCount": 1,
  "itemsFrom": 1,
  "itemsTo": 1
}
```

Errors:

- 400: validation/model binding
- 500: plain text error message

#### GET /api/restaurants/{id}

Description:
Returns a restaurant by id.

Headers:
None required.

Response (200):

```json
{
  "id": 1,
  "name": "Gourmet Delights",
  "description": "A culinary experience",
  "category": "American",
  "hasDelivery": true,
  "city": "Metropolis",
  "street": "456 Gourmet Avenue",
  "postalCode": "54-551",
  "dishes": [
    {
      "id": 10,
      "name": "Burger",
      "description": "Juicy burger",
      "price": 8.5,
      "kiloCalories": 650,
      "restaurantId": 1
    }
  ]
}
```

Errors:

- 404: plain text message
- 500: plain text message

#### POST /api/restaurants

Description:
Creates a restaurant.

Headers:
Authorization: Bearer {token}

Roles:

- `Admin`

Request:

```json
{
  "name": "Gourmet Delights",
  "description": "A culinary experience",
  "category": "American",
  "hasDelivery": true,
  "contactEmail": "info@gourmet.com",
  "contactNumber": "01234567890",
  "city": "Metropolis",
  "street": "456 Gourmet Avenue",
  "postalCode": "54-551"
}
```

Validation:

- `name`: required, length 3-100
- `description`: required
- `category`: must be one of `Italian`, `Mexican`, `Japanese`, `American`, `Indian`
- `contactEmail`: must be valid email if provided
- `contactNumber`: 11 digits if provided
- `postalCode`: format `NN-NNN` if provided

Response (201):

- `Location` header set; body is `null`.

Errors:

- 400: validation/model binding
- 401: unauthorized
- 403: plain text "Access forbidden"
- 500: plain text message

#### PATCH /api/restaurants/{id}

Description:
Updates a restaurant.

Headers:
Authorization: Bearer {token}

Roles:

- `Owner`

Request:

```json
{
  "name": "Updated Name",
  "description": "Updated description",
  "category": "American",
  "hasDelivery": true,
  "contactEmail": "info@gourmet.com",
  "contactNumber": "01234567890",
  "city": "Metropolis",
  "street": "456 Gourmet Avenue",
  "postalCode": "54-551"
}
```

Validation:

- `name`: length 3-100 (if provided)
- `category`: must be valid (if provided)

Response (204): No content.

Errors:

- 400: validation/model binding
- 401: unauthorized
- 403: plain text "Access forbidden"
- 404: plain text message
- 500: plain text message

#### DELETE /api/restaurants/{id}

Description:
Deletes a restaurant.

Headers:
Authorization: Bearer {token}

Roles:

- `Admin`

Response (204): No content.

Errors:

- 401: unauthorized
- 403: plain text "Access forbidden"
- 404: plain text message
- 500: plain text message

### Dishes (by Restaurant)

#### POST /api/restaurants/{restaurantId}/dishes

Description:
Creates a dish for a restaurant.

Headers:
Content-Type: application/json

Request:

```json
{
  "name": "Burger",
  "description": "Juicy burger",
  "price": 8.5,
  "kiloCalories": 650
}
```

Validation:

- `price` >= 0
- `kiloCalories` >= 0 (if provided)

Response (201):

- `Location` header set; body is `null`.

Errors:

- 400: validation/model binding
- 500: plain text message

#### GET /api/restaurants/{restaurantId}/dishes

Description:
Returns all dishes for a restaurant.

Headers:
None required.

Response (200):

```json
[
  {
    "id": 10,
    "name": "Burger",
    "description": "Juicy burger",
    "price": 8.5,
    "kiloCalories": 650
  }
]
```

Errors:

- 404: plain text message
- 500: plain text message

#### GET /api/restaurants/{restaurantId}/dishes/{dishId}

Description:
Returns a dish by id for a restaurant.

Headers:
None required.

Response (200):

```json
{
  "id": 10,
  "name": "Burger",
  "description": "Juicy burger",
  "price": 8.5,
  "kiloCalories": 650
}
```

Errors:

- 404: plain text message
- 500: plain text message

#### PUT /api/restaurants/{restaurantId}/dishes/{dishId}

Description:
Updates a dish for a restaurant.

Headers:
Content-Type: application/json

Request:

```json
{
  "id": 10,
  "name": "Updated Burger",
  "description": "Updated description",
  "price": 9.0,
  "kiloCalories": 700
}
```

Validation:

- `price` >= 0
- `kiloCalories` >= 0 (if provided)

Response (204): No content.

Errors:

- 400: validation/model binding
- 403: plain text "Access forbidden"
- 404: plain text message
- 500: plain text message

#### DELETE /api/restaurants/{restaurantId}/dishes/{dishId}

Description:
Deletes a dish for a restaurant.

Headers:
None required.

Response (204): No content.

Errors:

- 404: plain text message
- 500: plain text message

### Cart (Authenticated)

#### GET /api/cart

Description:
Returns the current user's cart.

Headers:
Authorization: Bearer {token}

Response (200):

```json
{
  "id": 1,
  "items": [
    {
      "id": 5,
      "dishId": 10,
      "quantity": 2
    }
  ]
}
```

Errors:

- 401: unauthorized
- 500: plain text message

#### POST /api/cart/items

Description:
Adds an item to the current cart.

Headers:
Authorization: Bearer {token}

Request:

```json
{
  "dishId": 10,
  "quantity": 2
}
```

Validation:

- `dishId` > 0
- `quantity` > 0

Response (204): No content.

Errors:

- 400: validation/model binding
- 401: unauthorized
- 500: plain text message

#### PATCH /api/cart/items/{id}

Description:
Updates an item quantity in the current cart.

Headers:
Authorization: Bearer {token}

Request:

```json
{
  "quantity": 3
}
```

Validation:

- `id` > 0 (route)
- `quantity` > 0

Response (204): No content.

Errors:

- 400: validation/model binding
- 401: unauthorized
- 500: plain text message

#### DELETE /api/cart/items/{id}

Description:
Removes an item from the current cart.

Headers:
Authorization: Bearer {token}

Response (204): No content.

Errors:

- 400: validation/model binding
- 401: unauthorized
- 500: plain text message

### Orders (Authenticated)

#### GET /api/orders/{id}

Description:
Returns an order by id.

Headers:
Authorization: Bearer {token}

Response (200):

```json
{
  "id": 100,
  "createdAt": "2026-04-29T10:15:30Z",
  "status": "Pending",
  "totalAmount": 25.0,
  "items": [
    {
      "id": 1,
      "name": "Burger",
      "quantity": 2,
      "price": 8.5,
      "orderId": 100
    }
  ],
  "customerId": "user-123"
}
```

Errors:

- 401: unauthorized
- 404: plain text message
- 500: plain text message

#### POST /api/orders

Description:
Creates an order directly (not via checkout).

Headers:
Authorization: Bearer {token}

Request:

```json
{
  "items": [
    {
      "id": 0,
      "name": "Burger",
      "quantity": 2,
      "price": 8.5,
      "orderId": 0
    }
  ]
}
```

Response (201):

- `Location` header set; body is `null`.

Errors:

- 400: validation/model binding
- 401: unauthorized
- 500: plain text message

#### POST /api/orders/checkout

Description:
Creates an order from the cart and optionally generates a Paymob payment URL.

Headers:
Authorization: Bearer {token}

Request:

```json
{
  "paymentMethod": "Cash"
}
```

Response (200):

```json
{
  "orderId": 123,
  "paymentUrl": "https://accept.paymob.com/api/acceptance/iframes/{iframeId}?payment_token=..."
}
```

Errors:

- 400: validation/model binding
- 401: unauthorized
- 404: plain text message
- 500: plain text message

#### PATCH /api/orders/{id}/status

Description:
Updates an order status.

Headers:
Authorization: Bearer {token}

Request:

```json
{
  "status": "Preparing"
}
```

Validation:

- `status` must be a valid `OrderStatus` value

Response (204): No content.

Errors:

- 400: validation/model binding
- 401: unauthorized
- 404: plain text message
- 500: plain text message

### Payments

#### POST /api/payments/callback

Description:
Paymob callback to mark payment status.

Headers:
Content-Type: application/json

Request:

```json
{
  "orderId": 123,
  "success": true
}
```

Behavior:

- `success=true` -> `PaymentStatus.Paid`
- `success=false` -> `PaymentStatus.Failed`

Response (200): empty body.

Errors:

- 400: validation/model binding
- 500: plain text message

### Identity (Custom Controller)

#### PATCH /api/identity/user

Description:
Updates the current user's details.

Headers:
None required (explicitly `AllowAnonymous`).

Request:

```json
{
  "nationality": "Egyptian",
  "dateOfBirth": "1995-06-10"
}
```

Response (204): No content.

Errors:

- 404: plain text message
- 500: plain text message

#### POST /api/identity/userRole

Description:
Assigns a role to a user.

Headers:
Authorization: Bearer {token}

Roles:

- `Admin`

Request:

```json
{
  "userEmail": "user@test.com",
  "roleName": "Owner"
}
```

Response (204): No content.

Errors:

- 401: unauthorized
- 403: plain text "Access forbidden"
- 500: plain text message

#### DELETE /api/identity/userRole

Description:
Unassigns a role from a user.

Headers:
Authorization: Bearer {token}

Roles:

- `Admin`

Request:

```json
{
  "userEmail": "user@test.com",
  "roleName": "Owner"
}
```

Response (204): No content.

Errors:

- 401: unauthorized
- 403: plain text "Access forbidden"
- 404: plain text message
- 500: plain text message

### Identity (Minimal API)

The built-in ASP.NET Core Identity endpoints are registered under `/api/identity` via `MapIdentityApi<User>()`. These endpoints are defined by the framework (register, login, refresh, manage) and are not explicitly implemented in this repo.

### Special Flows

Checkout Flow (Cash vs Paymob):

1. User adds items to cart.
2. Call `POST /api/orders/checkout` with `paymentMethod`.
3. Server creates an order from cart items.
4. If `paymentMethod=Paymob`, server returns `paymentUrl`.
5. If `paymentMethod=Cash`, `paymentUrl` is `null`.

Payment Callback:

- Paymob calls `POST /api/payments/callback`.
- Backend marks payment as `Paid` or `Failed`.

## Build and Test

```bash
dotnet build
dotnet test
```

## License

This project does not currently include a license file.
