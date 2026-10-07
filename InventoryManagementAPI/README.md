# Inventory Management API

A role-based Inventory Management REST API built with ASP.NET Core Web API, Entity Framework Core, and SQL Server.

## Features

- JWT Authentication
- Role-Based Authorization
- Admin, Manager, and Employee roles
- Product Management
- Category Management
- Supplier Management
- Stock Management
- Transaction Management
- Inventory Reports
- DTO-based API responses
- Search
- Filtering
- Pagination
- Input Validation
- Global Exception Handling
- SQL Server Database
- Entity Framework Core
- Database-First Approach
- OpenAPI support
- Postman API Testing

## Technology Stack

- ASP.NET Core Web API
- .NET 8
- Entity Framework Core 8
- SQL Server
- JWT Bearer Authentication
- C#
- Visual Studio
- Postman

## Project Structure

```text
InventoryManagementAPI
│
├── Controllers
│   ├── AccountController.cs
│   ├── ProductsController.cs
│   ├── CategoriesController.cs
│   ├── SuppliersController.cs
│   ├── StocksController.cs
│   ├── TransactionsController.cs
│   └── ReportsController.cs
│
├── Data
│   ├── InventoryManagementDbContext.cs
│   └── Entities
│
├── DTOS
│   ├── ProductDTO.cs
│   ├── ProductSaveDTO.cs
│   ├── CategoryDTO.cs
│   ├── CategorySaveDTO.cs
│   ├── SupplierDTO.cs
│   ├── SupplierSaveDTO.cs
│   ├── StockDTO.cs
│   ├── StockSaveDTO.cs
│   ├── TransactionDTO.cs
│   └── TransactionSaveDTO.cs
│
├── Middleware
│   └── ExceptionMiddleware.cs
│
├── Program.cs
└── appsettings.json
```

## Roles and Permissions

| Module | Admin | Manager | Employee |
|---|---|---|---|
| Products | Yes | Yes | No |
| Categories | Yes | Yes | No |
| Suppliers | Yes | Yes | No |
| Stock | Yes | Yes | Yes |
| Transactions | Yes | Yes | Yes |
| Reports | Yes | Yes | No |

## Authentication

Login endpoint:

```text
POST /api/Account/Login
```

Example:

```text
POST /api/Account/Login?email=admin@gmail.com&password=Admin@123
```

The API returns a JWT token. The token is used as a Bearer Token for protected endpoints.

## Product API

### Get All Products

```text
GET /api/Products
```

### Search Products

```text
GET /api/Products?search=Laptop
```

### Filter by Category

```text
GET /api/Products?categoryId=2
```

### Filter by Supplier

```text
GET /api/Products?supplierId=3
```

### Pagination

```text
GET /api/Products?page=1&pageSize=3
```

## Reports

### Inventory Summary

```text
GET /api/Reports/Summary
```

Provides:

- Total Products
- Total Categories
- Total Suppliers
- Total Stock
- Total Purchases
- Total Sales

### Low Stock Report

```text
GET /api/Reports/LowStock?threshold=5
```

Returns products whose stock quantity is equal to or below the specified threshold.

## Database

The project uses **SQL Server** with a **Database-First** approach.

Database:

```text
InventoryManagementDb
```

Main tables:

- Users
- Categories
- Suppliers
- Products
- Stock
- Transactions

## Error Handling

Global exception handling is implemented using custom middleware.

Unexpected server errors return a standardized response:

```json
{
  "statusCode": 500,
  "message": "An unexpected error occurred."
}
```

## Testing

The API was tested using Postman, including:

- User login
- JWT authentication
- Role-based authorization
- Product CRUD
- Category CRUD
- Supplier CRUD
- Stock operations
- Transaction operations
- Product search
- Category filtering
- Supplier filtering
- Pagination
- Inventory reports
- Unauthorized/forbidden access testing

## Author

**Md. Abir Ahmed**

AIUB — B.Sc. in Computer Science