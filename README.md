📚 Library Management API

A RESTful API built with ASP.NET Core (.NET 8) for managing a library system including books, authors, categories, members, and borrowing operations.

This project focuses on backend architecture, clean code structure, and scalable design practices.

🚀 Tech Stack
ASP.NET Core Web API (.NET 8)
Entity Framework Core
SQL Server
AutoMapper
Swagger (OpenAPI)
CORS
🧱 Architecture Overview

The project is structured with separation of concerns in mind:

Controllers → Services → Data Access

Planned improvements include moving toward Clean Architecture with:

Repository Pattern
Middleware for global error handling
DTO-based communication
📁 Project Structure
Controllers/
Models/
DTOs/
Services/
Data/
Mappings/
⚙️ Features
📚 Books
Create, Read, Update, Delete
Filter by Category or Author
Unique title per author
Quantity management
🏷 Categories & Authors
Full CRUD operations
👤 Members
CRUD operations
📖 Borrowing System
Borrow books (decreases quantity)
Return books (increases quantity)
Prevent borrowing if quantity = 0
🧠 Business Rules
Book title is required
Book title must be unique per author
Price cannot be negative
Quantity cannot be less than 0
Cannot borrow unavailable books
Return date cannot be earlier than borrow date
🧩 Design Decisions
DTOs used instead of exposing entities
AutoMapper used for clean object mapping
Services layer handles business logic
Controllers kept thin and simple
Dependency Injection used across the project
🧪 Testing

All endpoints tested using:

Swagger UI
Postman

Verified responses for:

200 OK
201 Created
400 Bad Request
404 Not Found
📌 Swagger UI

After running the project:

https://localhost:<port>/swagger
🌐 CORS

CORS is enabled to allow frontend integration during development.

🧭 Current Status

This project is in active development.

Planned refactoring includes:

Repository Pattern implementation
Global Exception Middleware
JWT Authentication
Pagination & Search
📦 Setup & Run
git clone <repo-url>
cd LibraryAPI
dotnet restore
dotnet ef database update
dotnet run
🚀 Future Improvements
JWT Authentication & Role-based Authorization
Pagination & Filtering
Soft Delete
Logging & Monitoring
File Upload (Book Covers)
Clean Architecture refactor (Repository + Infrastructure separation)
🧠 Note

This project is focused on learning and applying backend fundamentals with ASP.NET Core, and will be gradually refactored toward a production-level architecture.