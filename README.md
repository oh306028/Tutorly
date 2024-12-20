## Tutorly API Documentation

### Project Overview
The **Tutorly API** is a web application connecting students with tutors. The platform allows students to search for tutors based on subjects, locations, and availability. Tutors can create profiles, post listings detailing their expertise, pricing, and schedules, while students can apply for sessions via these listings. The API implements **Clean Architecture** and follows the **CQRS (Command Query Responsibility Segregation)** design pattern.

### Key Features:
- **Authentication**: JWT-based authentication ensures secure access control.
- **Fluent Validation**: Used for input validation across endpoints.
- **AutoMapper**: Assists in mapping between domain models and data transfer objects (DTOs).
- **Test Coverage**: The project includes both **unit tests** and **integration tests** using XUnit.

---

### API Endpoints

#### **Account Endpoints**
- **POST** `api/accounts/login`:  
  Authenticates a user and returns a JWT token.
  
- **POST** `api/accounts/register`:  
  Registers a new user.
  
- **PATCH** `api/accounts/{userId}`:  
  Updates user details.
  
- **GET** `api/accounts/{userId}`:  
  Retrieves user details.
  
- **DELETE** `api/accounts/{userId}`:  
  Deletes a user account.

---

#### **Category Endpoints**
- **GET** `api/categories`:  
  Retrieves a list of all available categories.
  
- **POST** `api/categories`:  
  (Admin-only) Adds a new category.

---

#### **Post Endpoints**
- **GET** `api/posts`:  
  Retrieves all available tutoring posts.
  
- **POST** `api/posts`:  
  (Tutor-only) Creates a new tutoring post.
  
- **POST** `api/posts/{postId}`:  
  (Student-only) Applies for a tutoring post.

  - **POST** `api/posts/{postId}/accept`:  
  (Tutor-only) Accepts student for a post application.

  - **POST** `api/posts/{postId}/decline`:  
  (Tutor-only) Decline student from a post application.


- **DELETE** `api/posts/{postId}`:  
  Deletes a tutoring post.

---

#### **Tutor Endpoints**
- **GET** `api/tutor`:  
  Retrieves a list of all tutors.
  
- **GET** `api/tutor/{id}`:  
  Retrieves details of a specific tutor.

---

### Authentication
- **JWT Authentication**: All endpoints are secured with JWT-based authentication. A valid token must be provided in the request headers.

### Tools and Libraries Used:
- **FluentValidation**: Used for request validation ensuring only valid data is processed.
- **FluentAssertions**: Employed for validating the behavior and outputs in unit and integration tests.
- **AutoMapper**: Assists in mapping between domain models and DTOs, ensuring efficient data transfer.
- **EntityFramework**: ORM technology used to create a database context and configure entities during application runtime. (code first)
- **MSSQL**: database server used to store users and posts, configured to work with EF. Tested locally -> in future should be displayed to the Azure SQL.

### Testing
- The project includes comprehensive **unit tests** and **integration tests** using XUnit, covering various aspects such as validation, data transformation, and API responses. (used FluentAssertions)
