# Restaurants API

## Overview
ASP.NET Core Web API for restaurant management and ordering built with Clean Architecture.
Includes restaurant/dish catalogs, carts, orders, and payments with role-based access.

## Features
- Restaurant and dish CRUD with paging and filtering
- Cart management and checkout (cash or Paymob)
- Order creation and status tracking
- Role-based authorization and Identity endpoints
- Seed data for roles and sample restaurants
- Swagger/OpenAPI docs and Serilog logging

## Tech Stack
- **Framework:** .NET 9, ASP.NET Core
- **Architecture:** Clean Architecture, CQRS with MediatR
- **Data:** Entity Framework Core, SQL Server
- **Security:** ASP.NET Core Identity, JWT Bearer
- **Utilities:** AutoMapper, FluentValidation, Serilog, Swagger/OpenAPI
