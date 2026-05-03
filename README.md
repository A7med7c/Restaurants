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

## Angular API (TypeScript)

Assumes `environment.ts` contains:

```ts
export const environment = {
  apiBaseUrl: "https://localhost:7117",
};
```

### Enums

```ts
export enum PaymentMethod {
  Cash = "Cash",
  Paymob = "Paymob",
}

export enum PaymentStatus {
  Pending = "Pending",
  Paid = "Paid",
  Failed = "Failed",
}

export enum OrderStatus {
  Pending = "Pending",
  Confirmed = "Confirmed",
  Preparing = "Preparing",
  Ready = "Ready",
  OnTheWay = "OnTheWay",
  Delivered = "Delivered",
  Cancelled = "Cancelled",
}

export enum SortDirection {
  Ascending = "Ascending",
  Descending = "Descending",
}
```

### Interfaces (DTOs and Commands)

```ts
export interface PagedResult<T> {
  items: T[];
  totalPages: number;
  totalItemsCount: number;
  itemsFrom: number;
  itemsTo: number;
}

export interface Dish {
  id: number;
  name: string;
  description: string;
  price: number;
  kiloCalories?: number | null;
  restaurantId: number;
}

export interface RestaurantDto {
  id: number;
  name: string;
  description: string;
  category: string;
  hasDelivery: boolean;
  city?: string | null;
  street?: string | null;
  postalCode?: string | null;
  dishes: Dish[];
}

export interface DishesDto {
  id: number;
  name: string;
  description: string;
  price: number;
  kiloCalories?: number | null;
}

export interface CartItemDto {
  id: number;
  dishId: number;
  quantity: number;
}

export interface CartDto {
  id: number;
  items: CartItemDto[];
}

export interface OrderItem {
  id: number;
  name: string;
  quantity: number;
  price: number;
  orderId: number;
}

export interface OrdersDto {
  id: number;
  createdAt: string;
  status: OrderStatus;
  totalAmount: number;
  items: OrderItem[];
  customerId: string;
}

export interface CheckoutResultDto {
  orderId: number;
  paymentUrl?: string | null;
}

export interface CreateRestaurantCommand {
  name: string;
  description: string;
  category: string;
  hasDelivery: boolean;
  contactEmail?: string | null;
  contactNumber?: string | null;
  city?: string | null;
  street?: string | null;
  postalCode?: string | null;
}

export interface UpdateRestaurantCommand extends CreateRestaurantCommand {
  id: number;
}

export interface CreateDishForRestaurantCommand {
  name: string;
  description: string;
  price: number;
  kiloCalories?: number | null;
}

export interface UpdateDishForRestaurantCommand {
  name: string;
  description: string;
  price: number;
  kiloCalories?: number | null;
}

export interface AddToCartCommand {
  dishId: number;
  quantity: number;
}

export interface UpdateCartItemCommand {
  id: number;
  quantity: number;
}

export interface CreateOrderCommand {
  items: OrderItem[];
}

export interface CheckoutCommand {
  paymentMethod: PaymentMethod;
}

export interface UpdateOrderStatusCommand {
  status: OrderStatus;
}

export interface PaymobCallbackRequest {
  orderId: number;
  success: boolean;
}

export interface UpdateUserDetailsCommand {
  nationality?: string | null;
  dateOfBirth?: string | null;
}

export interface AssignUserRoleCommand {
  userEmail: string;
  roleName: string;
}

export interface UnAssignUserRoleCommand {
  userEmail: string;
  roleName: string;
}
```

### Angular Services

```ts
import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../environments/environment";

@Injectable({ providedIn: "root" })
export class RestaurantsApi {
  private baseUrl = `${environment.apiBaseUrl}/api/restaurants`;

  constructor(private http: HttpClient) {}

  getAll(params: {
    searchPhrase?: string;
    pageNumber?: number;
    pageSize?: number;
    sortDirection?: SortDirection;
    sortBy?: string;
  }) {
    let httpParams = new HttpParams();
    Object.entries(params).forEach(([key, value]) => {
      if (value !== undefined && value !== null) {
        httpParams = httpParams.set(key, String(value));
      }
    });
    return this.http.get<PagedResult<RestaurantDto>>(this.baseUrl, {
      params: httpParams,
    });
  }

  getById(id: number) {
    return this.http.get<RestaurantDto>(`${this.baseUrl}/${id}`);
  }

  create(command: CreateRestaurantCommand) {
    return this.http.post<void>(this.baseUrl, command);
  }

  update(id: number, command: UpdateRestaurantCommand) {
    return this.http.patch<void>(`${this.baseUrl}/${id}`, command);
  }

  delete(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}

@Injectable({ providedIn: "root" })
export class DishesApi {
  constructor(private http: HttpClient) {}

  create(restaurantId: number, command: CreateDishForRestaurantCommand) {
    return this.http.post<void>(
      `${environment.apiBaseUrl}/api/restaurants/${restaurantId}/dishes`,
      command,
    );
  }

  getAll(restaurantId: number) {
    return this.http.get<DishesDto[]>(
      `${environment.apiBaseUrl}/api/restaurants/${restaurantId}/dishes`,
    );
  }

  getById(restaurantId: number, dishId: number) {
    return this.http.get<DishesDto>(
      `${environment.apiBaseUrl}/api/restaurants/${restaurantId}/dishes/${dishId}`,
    );
  }

  update(
    restaurantId: number,
    dishId: number,
    command: UpdateDishForRestaurantCommand,
  ) {
    return this.http.put<void>(
      `${environment.apiBaseUrl}/api/restaurants/${restaurantId}/dishes/${dishId}`,
      command,
    );
  }

  delete(restaurantId: number, dishId: number) {
    return this.http.delete<void>(
      `${environment.apiBaseUrl}/api/restaurants/${restaurantId}/dishes/${dishId}`,
    );
  }
}

@Injectable({ providedIn: "root" })
export class CartApi {
  private baseUrl = `${environment.apiBaseUrl}/api/cart`;

  constructor(private http: HttpClient) {}

  getCurrent() {
    return this.http.get<CartDto>(this.baseUrl);
  }

  addItem(command: AddToCartCommand) {
    return this.http.post<void>(`${this.baseUrl}/items`, command);
  }

  updateItem(id: number, command: UpdateCartItemCommand) {
    return this.http.patch<void>(`${this.baseUrl}/items/${id}`, command);
  }

  removeItem(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/items/${id}`);
  }
}

@Injectable({ providedIn: "root" })
export class OrdersApi {
  private baseUrl = `${environment.apiBaseUrl}/api/orders`;

  constructor(private http: HttpClient) {}

  getById(id: number) {
    return this.http.get<OrdersDto>(`${this.baseUrl}/${id}`);
  }

  create(command: CreateOrderCommand) {
    return this.http.post<void>(this.baseUrl, command);
  }

  checkout(command: CheckoutCommand) {
    return this.http.post<CheckoutResultDto>(
      `${this.baseUrl}/checkout`,
      command,
    );
  }

  updateStatus(id: number, command: UpdateOrderStatusCommand) {
    return this.http.patch<void>(`${this.baseUrl}/${id}/status`, command);
  }
}

@Injectable({ providedIn: "root" })
export class PaymentsApi {
  private baseUrl = `${environment.apiBaseUrl}/api/payments`;

  constructor(private http: HttpClient) {}

  handlePaymobCallback(request: PaymobCallbackRequest) {
    return this.http.post<void>(`${this.baseUrl}/callback`, request);
  }
}

@Injectable({ providedIn: "root" })
export class IdentityApi {
  private baseUrl = `${environment.apiBaseUrl}/api/identity`;

  constructor(private http: HttpClient) {}

  updateUserDetails(command: UpdateUserDetailsCommand) {
    return this.http.patch<void>(`${this.baseUrl}/user`, command);
  }

  assignUserRole(command: AssignUserRoleCommand) {
    return this.http.post<void>(`${this.baseUrl}/userRole`, command);
  }

  unassignUserRole(command: UnAssignUserRoleCommand) {
    return this.http.delete<void>(`${this.baseUrl}/userRole`, {
      body: command,
    });
  }
}
```

## Build and Test

```bash
dotnet build
dotnet test
```

## License

This project does not currently include a license file.
