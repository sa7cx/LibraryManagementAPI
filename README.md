📚 Library Management API (ASP.NET Core Web API)

مشروع API لإدارة مكتبة يحتوي على إدارة الكتب، المؤلفين، التصنيفات، الأعضاء وعمليات الاستعارة والإرجاع باستخدام ASP.NET Core و Entity Framework Core.

🚀 Technologies Used
ASP.NET Core Web API (.NET 8)
Entity Framework Core
SQL Server
AutoMapper
Swagger (OpenAPI)
CORS
📁 Project Structure
Controllers/
Models/
DTOs/
Services/
Data/
Mappings/
⚙️ How to Run the Project
1. Clone the repository
git clone <repo-url>
2. Restore packages
dotnet restore
3. Update database
dotnet ef database update
4. Run the project
dotnet run
📌 Swagger

بعد تشغيل المشروع:

https://localhost:<port>/swagger

من خلال Swagger يمكنك تجربة جميع الـ endpoints مباشرة.

🌐 CORS Configuration

تم تفعيل CORS للسماح بالاتصال مع أي Frontend client خلال التطوير.

🗄️ Database Entities
Category
Author
Book
Member
BorrowRecord
العلاقات:
Category → Books (One-to-Many)
Author → Books (One-to-Many)
Member → BorrowRecords (One-to-Many)
Book → BorrowRecords (One-to-Many)
🔄 Features Implemented
📂 Categories & Authors
Create / Read / Update / Delete
📚 Books
CRUD operations
Filter by CategoryId
Filter by AuthorId
👤 Members
CRUD operations
📖 Borrowing System
Borrow a book (decreases quantity)
Return a book (increases quantity)
🧠 Business Rules
Book title is required
Book title must be unique per author
Price cannot be negative
Quantity cannot be less than 0
Cannot borrow a book if quantity = 0
Return date cannot be earlier than borrow date
🧩 Architecture Improvements
Controllers are kept thin
Business logic moved to Services layer
Dependency Injection used for services
DTOs used instead of exposing entities
AutoMapper used for mapping between Models and DTOs
📦 DTOs & AutoMapper
Request & Response DTOs implemented for main entities
AutoMapper profiles configured for clean mapping
Entities are not exposed directly from API responses
🧪 Testing

All endpoints tested using:

Swagger UI
Postman

Checked responses for:

200 OK
201 Created
400 Bad Request
404 Not Found
🧾 Notes
JWT Authentication not implemented (project is focused on backend structure and CRUD operations)
Bonus features partially implemented depending on scope
📌 Future Improvements (Optional)
JWT Authentication & Role-based authorization
Pagination & Search for books
Soft Delete for entities
Audit logging
File upload for book covers
