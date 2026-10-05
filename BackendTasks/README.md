# 💻 BackendTasks — Enterprise Backend Learning Portfolio

<p align="center">
  <a href="https://code.edu.az/" target="_blank" rel="noreferrer">
  </a>
</p>

<p align="center">
  <b>A professional backend practice repository built for real-world software engineering learning.</b><br/>
  Designed for Code Academy students to strengthen enterprise-level backend development skills through hands-on projects and structured practice.
</p>

<p align="center">
  <img alt=".NET" src="https://img.shields.io/badge/.NET-8.0-512BD4?logo=.net&logoColor=white" />
  <img alt="ASP.NET Core" src="https://img.shields.io/badge/ASP.NET%20Core-512BD4?logo=dotnet&logoColor=white" />
  <img alt="EF Core" src="https://img.shields.io/badge/EF%20Core-512BD4?logo=dotnet&logoColor=white" />
  <img alt="MS SQL" src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white" />
  <img alt="PostgreSQL" src="https://img.shields.io/badge/PostgreSQL-336791?logo=postgresql&logoColor=white" />
  <img alt="JWT" src="https://img.shields.io/badge/JWT-000000?logo=jsonwebtokens&logoColor=white" />
  <img alt="Swagger" src="https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=111111" />
</p>

---

# 📖 About The Repository

**BackendTasks** is a curated collection of backend-focused projects, exercises, and architectural implementations created during my backend engineering journey at **Code Academy**.

The primary goal of this repository is to practice and demonstrate:

- Enterprise Backend Architecture
- RESTful API Development
- Database Design & Management
- Authentication & Authorization
- Clean Code Principles
- Scalable Project Structures
- Real-world Backend Development Concepts

This repository also serves as a professional learning portfolio containing practical implementations of modern backend technologies and software engineering patterns.

---

# 🧩 Repository Structure

The repository contains multiple independent `.NET` solutions.  
Each folder focuses on a specific backend topic or architectural concept.

## Included Projects

| Project | Description |
|---|---|
| `FirstApiApp` | REST API fundamentals |
| `OnionArchApp` | Onion / Clean Architecture implementation |
| `PustokApp` | CRUD operations & EF Core |
| `MenuApp` | Repository Pattern & Dependency Injection |
| `EternaApp` | Validation, Services & Layered Architecture |
| `MentorApp` | Authentication & Authorization (JWT/Identity) |
| `RazorPagesApp` | Razor Pages practice |
| `TagHelpersExample` | ASP.NET Core Tag Helpers |
| `MessageBrokersMQ` | Message Broker integration & async communication |

> Some projects may also include lightweight frontend/demo layers using HTML, CSS, SCSS, and JavaScript.

---

# 🛠️ Technology Stack

## Backend
- ASP.NET Core
- .NET 8
- Entity Framework Core
- RESTful APIs
- Clean Architecture
- Onion Architecture
- Repository Pattern
- Unit of Work

## Database
- Microsoft SQL Server
- PostgreSQL

## Authentication & Security
- JWT Authentication
- ASP.NET Core Identity
- Authorization Policies

## Development Tools
- Swagger / OpenAPI
- EF Core Migrations
- LINQ
- Dependency Injection

## Frontend (Basic UI Layers)
- HTML5
- CSS3
- SCSS
- JavaScript

---

# 🏗️ Architecture Overview

Most projects follow a layered enterprise architecture structure:

```text
Core
 ├── Entities
 ├── DTOs
 ├── Interfaces

Infrastructure / Data
 ├── DbContext
 ├── Repositories
 ├── Migrations

Application / Business
 ├── Services
 ├── Validation
 ├── Business Logic

Presentation / WebAPI
 ├── Controllers
 ├── Middleware
 ├── Swagger
 ├── Authentication
```

### Main Objectives

- Maintain separation of concerns
- Build scalable applications
- Improve maintainability
- Increase testability
- Apply enterprise-grade architecture principles

---

# 📚 Learning Roadmap

| Week | Topic | Technologies |
|---|---|---|
| 1 | C#, OOP & SOLID Principles | C#, .NET |
| 2 | ASP.NET Core Fundamentals | ASP.NET Core, Swagger |
| 3 | CRUD Operations with EF Core | EF Core, SQL Server |
| 4 | Repository Pattern & Validation | LINQ, EF Core |
| 5 | Authentication & Authorization | JWT, Identity |
| 6 | Clean / Onion Architecture | Layered Architecture |
| 7 | Razor Pages & Tag Helpers | Razor Pages |
| 8 | Message Brokers & Async Systems | MQ Integration |

---

# ⚙️ Prerequisites

Before running the projects, make sure the following tools are installed:

- .NET SDK 8.0+
- SQL Server or PostgreSQL
- EF Core CLI Tools

Install EF CLI globally:

```bash
dotnet tool install --global dotnet-ef
```

---

# 🔧 Configuration

Update your `appsettings.json` or `appsettings.Development.json` file:

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=BackendTasksDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
  },

  "JwtSettings": {
    "Issuer": "BackendTasks",
    "Audience": "BackendTasks",
    "Key": "YourStrongJwtKeyHere"
  }
}
```

## PostgreSQL Example

```json
"Host=localhost;Database=BackendTasksDb;Username=postgres;Password=YourPassword;"
```

---

# 🗄️ Database Migration

Run migrations using the appropriate startup and infrastructure projects:

```bash
dotnet ef migrations add InitialCreate \
  --project <DataProjectPath> \
  --startup-project <WebApiProjectPath>
```

Update the database:

```bash
dotnet ef database update \
  --project <DataProjectPath> \
  --startup-project <WebApiProjectPath>
```

---

# ▶️ Running The Project

Restore packages:

```bash
dotnet restore
```

Run the application:

```bash
dotnet run --project <WebApiProjectPath>
```

---

# 📄 Swagger Documentation

After running the application, open:

```text
https://localhost:5001/swagger
```

---

# 🌱 Recommended Workflow

```text
1. Fork the repository
2. Create a feature branch
3. Implement the task/project
4. Commit clean and meaningful changes
5. Push your branch
6. Open a Pull Request
```

### Branch Naming Examples

```bash
feature/task-authentication
practice/repository-pattern
feature/onion-architecture
```

---

# ✅ Best Practices Encouraged

Projects are intentionally designed to be extendable.  
Students are encouraged to improve tasks by adding:

- Input Validation
- Exception Handling
- Logging
- Unit Testing
- Fluent Validation
- Layered Architecture
- Caching
- Async Programming
- Clean Code Principles

---

# 🔒 License

This repository is intended for educational and portfolio purposes.

Unless otherwise specified, all rights are reserved.

---

# ⭐ Final Note

This repository represents my practical backend development journey and focuses on applying real-world software engineering concepts using modern .NET technologies and enterprise architecture patterns.

It is continuously updated as new backend topics, projects, and architectural concepts are explored.
