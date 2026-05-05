# Restaurants API

## Overview
ASP.NET Core Web API for restaurant management and ordering.
Built with Clean Architecture and CQRS to support catalogs, carts, orders, and payments.

## Features
- Restaurant and dish catalog CRUD with paging and filtering
- Cart management and checkout (cash, Paymob)
- Order creation and status tracking
- Role-based authorization and Identity endpoints
- Seed data for roles and sample restaurants
- Swagger/OpenAPI documentation and Serilog logging

## Tech Stack
- **Framework:** .NET 9, ASP.NET Core
- **Architecture:** Clean Architecture, CQRS, MediatR
- **Data:** Entity Framework Core, SQL Server
- **Security:** ASP.NET Core Identity, JWT Bearer
- **Utilities:** AutoMapper, FluentValidation, Serilog, Swagger/OpenAPI
