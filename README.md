📚 Library Management API

A RESTful API built with ASP.NET Core Web API (.NET 8) for managing a library system including books, authors, categories, members, and borrowing operations.

The project focuses on Clean Architecture principles, separation of concerns, maintainable code, and secure authentication.

---

🚀 Tech Stack

- ASP.NET Core Web API (.NET 8)
- C#
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Bearer Authentication
- Swagger / OpenAPI
- CORS

---

🏗️ Architecture

The project follows a Clean Architecture-inspired structure, separating the application logic, domain models, infrastructure, and API layers.

LibraryManagementAPI
│
├── Application
│   ├── DTOs
│   ├── Exceptions
│   ├── Interfaces
│   │   ├── IRepositories
│   │   └── IServices
│   ├── Services
│   ├── .csAPIResponse
│   ├── ApiResponseT.cs
│   └── GenerateToken.cs
│
├── Domain
│   └── Models
│       └── Identity
│
├── Infrastructure
│   ├── Data
│   ├── Migrations
│   └── Repositories
│
└── LibraryManagementAPI
    ├── Controllers
    ├── Middleware
    ├── wwwroot
    ├── Program.cs
    └── appsettings.json

Layer Responsibilities

Domain

- Contains the core models of the application.
- Includes Identity-related models.

Application

- Contains business and application logic.
- Defines service and repository interfaces.
- Contains DTOs and custom exceptions.
- Contains application response models and JWT token generation.

Infrastructure

- Handles database access and persistence.
- Contains Entity Framework Core configuration, migrations, and repository implementations.

API

- Exposes the application through HTTP controllers.
- Contains middleware, Swagger, CORS, authentication configuration, and application startup configuration.

---

🔐 Authentication & Authorization

The API uses ASP.NET Core Identity together with JWT Bearer Authentication.

Authentication Features

- User registration
- User login
- Password validation using ASP.NET Core Identity
- JWT access token generation
- Bearer token authentication
- Role claims support

Authentication Flow

Client
   │
   ├── Register ──► Auth API ──► ASP.NET Core Identity
   │                              │
   │                              └── User Created
   │
   └── Login ────► Auth API ──► Identity validates credentials
                                  │
                                  └── JWT Access Token
                                        │
                                        ▼
                                  Protected Endpoints

For authenticated requests, the client sends the token using:

Authorization: Bearer <token>

---

⚙️ Features

📚 Books

- Create books
- Retrieve books
- Retrieve a book by ID
- Update books
- Delete books
- Filter books by category or author
- Manage book quantity
- Upload book cover images

🏷️ Authors & Categories

- Full CRUD operations
- Associate books with authors and categories

👤 Members

- Full CRUD operations
- Manage library members

📖 Borrowing System

- Borrow books
- Return books
- Decrease available quantity when borrowing
- Increase quantity when returning
- Prevent borrowing unavailable books

---

🧠 Business Rules

The API enforces business rules such as:

- Book title is required.
- Book title must be unique per author.
- Book price cannot be negative.
- Book quantity cannot be negative.
- Books cannot be borrowed when the available quantity is zero.
- Return date cannot be earlier than the borrow date.

Business logic is handled in the Application/Services layer while controllers remain focused on handling HTTP requests and responses.

---

🧩 Design Principles

Repository Pattern

Database access is abstracted through repository interfaces and their implementations.

Controller
    ↓
Service
    ↓
Repository Interface
    ↓
Repository
    ↓
Entity Framework Core
    ↓
SQL Server

DTOs

DTOs are used to control the data exchanged between the API and clients instead of directly exposing application models.

Dependency Injection

Services and repositories are registered and resolved using ASP.NET Core's built-in Dependency Injection system.

Global Exception Handling

A custom middleware handles unhandled exceptions and provides consistent API error responses.

---

🧪 API Testing

The API has been tested using:

- Swagger UI
- Postman

Tested responses include:

- "200 OK"
- "201 Created"
- "400 Bad Request"
- "401 Unauthorized"
- "404 Not Found"

Authentication endpoints are also tested using JWT Bearer tokens.

---

📌 Swagger / OpenAPI

After running the application, Swagger UI can be accessed through:

https://localhost:<port>/swagger

Swagger is configured to support Bearer Authentication, allowing protected endpoints to be tested directly from the Swagger UI.

---

🌐 CORS

CORS is enabled to allow frontend applications to communicate with the API during development.

---

📦 Setup & Run

1. Clone the repository

git clone https://github.com/sa7cx/LibraryManagementAPI.git

2. Navigate to the project

cd LibraryManagementAPI

3. Restore dependencies

dotnet restore

4. Apply database migrations

dotnet ef database update

5. Run the API

dotnet run

Then open Swagger:

https://localhost:<port>/swagger

---

🔒 Configuration

The application uses configuration files for database and authentication settings.

Sensitive configuration values such as database credentials and JWT signing keys should not be committed to source control and should be provided through secure configuration mechanisms in production environments.

---

🧭 Current Status

The project is actively being developed and progressively improved toward a production-ready backend architecture.

Completed

- RESTful API
- CRUD operations
- Entity Framework Core
- SQL Server
- DTO-based communication
- Service Layer
- Repository Pattern
- Dependency Injection
- Clean Architecture structure
- Global Exception Middleware
- Swagger / OpenAPI
- CORS
- ASP.NET Core Identity
- JWT Authentication
- User Registration
- User Login
- JWT Role Claims

Planned Improvements

- Pagination
- Advanced search and filtering
- Role-based authorization
- Soft Delete
- Logging and monitoring
- Refresh Tokens
- API versioning
- Docker support
- Production deployment

---

🎯 Project Goal

This project is focused on developing practical backend experience with ASP.NET Core and applying software architecture principles to a real-world REST API.

It is continuously evolving from a basic CRUD application into a more structured backend system with Clean Architecture, repository abstraction, centralized exception handling, Identity authentication, and JWT-based security.
