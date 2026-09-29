📚 Library Management API

<p align="center">
  A structured RESTful backend for managing books, authors, categories, members, and borrowing operations.
</p><p align="center">
  <img src="https://img.shields.io/badge/.NET-8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/EF%20Core-8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" />
</p><p align="center">
  <img src="https://img.shields.io/badge/ASP.NET%20Identity-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white" />
  <img src="https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" />
  <img src="https://img.shields.io/badge/Postman-FF6C37?style=for-the-badge&logo=postman&logoColor=white" />
</p>---

📌 Overview

Library Management API is a RESTful backend built with C# and ASP.NET Core for managing library operations such as books, authors, categories, members, and borrowing.

The project is designed to demonstrate practical backend engineering concepts beyond basic CRUD operations.

Key Concepts

- Clean Architecture-inspired structure
- Separation of concerns
- Service and Repository patterns
- DTO-based API design
- ASP.NET Core Identity
- JWT Authentication
- Business-rule validation
- Centralized exception handling
- Database transactions
- Optimistic concurrency
- Pagination
- Soft deletion

---

🚀 Features

📚 Books

- Create, retrieve, update, and delete books
- Retrieve books by ID
- Filter books by author or category
- Manage book quantity
- Pagination
- Soft deletion
- Validation
- Upload book cover images
- Supported formats: ".jpg", ".jpeg", ".png"

🏷️ Authors & Categories

- Full CRUD operations
- Book relationships
- Author/category filtering

👤 Members

- Full CRUD operations
- Manage library members
- Connect authenticated users with member profiles
- Track borrowing activity

📖 Borrowing System

The borrowing system handles the main library business rules:

- Identify the authenticated user
- Resolve the associated member
- Check book availability
- Decrease available quantity
- Create a borrowing record
- Execute related database operations transactionally
- Handle concurrency conflicts
- Restore quantity when returning a book

---

🏗️ Architecture

The project follows a Clean Architecture-inspired structure with a clear separation between the API, application logic, domain models, and infrastructure.

Project Structure
```text
LibraryManagementAPI/
│
├── Application/
│   ├── DTOs/
│   ├── Exceptions/
│   ├── Interfaces/
│   │   ├── IRepositories/
│   │   └── IServices/
│   ├── Services/
│   ├── ApiResponse/
│   └── GenerateToken.cs
│
├── Domain/
│   ├── Models/
│   └── Identity/
│       └── ApplicationUser.cs
│
├── Infrastructure/
│   ├── Data/
│   ├── Migrations/
│   └── Repositories/
│
├── LibraryManagementAPI/
│   ├── Controllers/
│   ├── Middleware/
│   ├── wwwroot/
│   ├── Program.cs
│   └── appsettings.json
│
├── LibraryManagementAPI.slnx
├── .gitignore
├── .gitattributes
└── README.md
```
Layer Responsibilities

API

Responsible for the HTTP layer and application configuration.

- Controllers
- Middleware
- Authentication configuration
- Swagger / OpenAPI
- CORS
- Dependency Injection
- Application startup

Application

Contains application and business logic.

- DTOs
- Services
- Service interfaces
- Repository interfaces
- Exceptions
- API response models
- JWT token generation

The Application layer works with abstractions and does not depend directly on Infrastructure implementations.

Domain

Contains the core models of the application.

- Domain entities
- Business models
- Identity models
- "ApplicationUser"

The custom "ApplicationUser" is located under:
```text
Domain/
└── Identity/
    └── ApplicationUser.cs
```
Infrastructure

Handles persistence and implementation details.

- Entity Framework Core
- SQL Server
- Database migrations
- Repository implementations
- Data access

---

🔄 Request Flow

A typical request passes through the application in the following order:

Client
  ↓
Controller
  ↓
Application Service
  ↓
Repository Interface
  ↓
Repository Implementation
  ↓
Entity Framework Core
  ↓
SQL Server

This separation keeps controllers focused on HTTP concerns while application services handle business logic and repositories handle persistence.

---

🔐 Authentication & Security

Authentication is implemented using ASP.NET Core Identity and JWT Bearer Authentication.

Authentication Components

- Custom "ApplicationUser"
- User registration
- User login
- Password validation
- JWT access-token generation
- Bearer authentication
- JWT claims
- Role claims
- Protected endpoints

Example authorization header:

Authorization: Bearer <JWT_TOKEN>

---

💾 Transactions & Data Consistency

Borrowing operations involve multiple database changes:

1. Check book availability
2. Decrease book quantity
3. Create borrowing record
4. Commit the transaction

If an operation fails, the transaction can be rolled back to prevent inconsistent data.

Begin Transaction
      ↓
Check Availability
      ↓
Decrease Quantity
      ↓
Create Borrow Record
      ↓
Commit Transaction

---

⚡ Optimistic Concurrency

The borrowing process handles concurrent updates when multiple users attempt to borrow the same book.

The project uses EF Core optimistic concurrency with a "RowVersion" concurrency token to detect conflicting updates.

This helps prevent incorrect book quantities when multiple requests target the same record simultaneously.

---

📄 Pagination

Large collections are retrieved using pagination rather than loading all records at once.

Example:

GET /api/books?pageNumber=1&pageSize=10

---

🗑️ Soft Delete

Instead of permanently deleting certain records, the application can mark them as deleted.

IsDeleted = true

This preserves the record while excluding it from normal queries.

---

📏 Business Rules

Examples of business rules implemented in the API:

- Books cannot be borrowed when available quantity is zero.
- Borrowing requires an authenticated user.
- The authenticated user must have an associated member profile.
- Book quantity is decreased after a successful borrowing operation.
- Returning a book restores its available quantity.
- Borrowing operations are handled transactionally.
- Concurrent updates are detected using optimistic concurrency.

---

🧩 Design Patterns & Practices

Repository Pattern

Abstracts database access behind repository interfaces and implementations.

Service Layer

Keeps application and business logic outside controllers.

DTOs

Controls the data exchanged between the API and clients without exposing domain models directly.

Dependency Injection

Uses ASP.NET Core's built-in Dependency Injection system.

Global Exception Handling

A centralized middleware handles unexpected exceptions and provides consistent API error responses.

Separation of Concerns

Each layer has a focused responsibility, making the application easier to maintain and extend.

---

🛠️ Tech Stack

Technology| Purpose
C#| Programming Language
.NET 8| Application Framework
ASP.NET Core Web API| REST API
Entity Framework Core| ORM
SQL Server| Database
ASP.NET Core Identity| Authentication & User Management
JWT| Authentication
Swagger / OpenAPI| API Documentation
Postman| API Testing
Git / GitHub| Version Control

---

🧪 API Testing

The API can be tested using:

- Swagger UI
- Postman

Common HTTP Status Codes

Status Code| Meaning
"200"| OK
"201"| Created
"204"| No Content
"400"| Bad Request
"401"| Unauthorized
"403"| Forbidden
"404"| Not Found
"409"| Conflict
"500"| Internal Server Error

---

📘 Swagger / OpenAPI

Swagger provides interactive API documentation and allows endpoints to be tested directly from the browser.

JWT Bearer authentication is also configured for testing protected endpoints through Swagger UI.

---

🌐 CORS

CORS is configured to allow external clients to communicate with the API.

---

⚙️ Getting Started

Prerequisites

- .NET 8 SDK
- SQL Server
- SQL Server Management Studio or another SQL client
- Git

Clone the Repository

git clone https://github.com/sa7cx/LibraryManagementAPI.git
cd LibraryManagementAPI

Restore Dependencies

dotnet restore

Configure the Database

Update the database connection string in:

appsettings.json

Apply Migrations

dotnet ef database update

Run the API

dotnet run

Then open Swagger using the URL displayed by ASP.NET Core.

---

🧭 Project Status

✅ Completed

- RESTful API
- CRUD operations
- Entity Framework Core
- SQL Server
- DTO-based API design
- Service Layer
- Repository Pattern
- Dependency Injection
- Clean Architecture-inspired structure
- Global Exception Middleware
- Swagger / OpenAPI
- CORS
- ASP.NET Core Identity
- Custom "ApplicationUser"
- User Registration
- User Login
- JWT Authentication
- JWT Role Claims
- Pagination
- Soft Delete
- Book Cover Uploads
- Borrowing System
- Returning Books
- Database Transactions
- Optimistic Concurrency

🔜 Planned

- Role-based authorization
- Advanced search and filtering
- Structured logging and monitoring
- Refresh Tokens
- Automated unit and integration testing
- API versioning
- Docker support
- Production deployment

---

🎯 Project Goal

This project was built to develop and demonstrate practical backend engineering skills using C# and ASP.NET Core.

The goal is to go beyond basic CRUD and apply concepts such as:

- Layered architecture
- Business logic separation
- Authentication
- Data consistency
- Transactions
- Concurrency handling
- Repository abstraction
- Maintainable API design

---

👨‍💻 Author

Salah Al-Din Al Ali

Junior .NET Developer focused on backend development and data-driven applications.

Core Technologies

- C#
- .NET
- ASP.NET Core
- Entity Framework Core
- SQL Server
- REST APIs
- ASP.NET Core Identity
- JWT

---

<p align="center">
  Built with C# & ASP.NET Core 🚀
</p>
