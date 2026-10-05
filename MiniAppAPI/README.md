# 🎟️ MiniAppAPI

MiniAppAPI is a modern ASP.NET Core Web API project built with **.NET 10** for managing events, organizers, and ticket operations.

The application provides a scalable RESTful API architecture with validation, pagination, file upload support, standardized responses, and OpenAPI documentation.

---

# 📌 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Requirements](#requirements)
- [Configuration](#configuration)
- [Installation & Running](#installation--running)
- [API Documentation](#api-documentation)
- [Core Endpoints](#core-endpoints)
- [Pagination & Sorting](#pagination--sorting)
- [Request Examples](#request-examples)
- [Response Structure](#response-structure)
- [Error Handling](#error-handling)
- [File Upload Notes](#file-upload-notes)
- [Troubleshooting](#troubleshooting)
- [Development Flow](#development-flow)

---

# 📖 Overview

MiniAppAPI is designed to manage:

- Event operations
- Organizer management
- Ticket management
- File uploads
- Pagination & sorting
- API response standardization

The project follows clean API architecture principles and includes reusable middleware, DTO mapping, and centralized validation mechanisms.

---

# ✨ Features

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core 10
- PostgreSQL integration via `Npgsql`
- FluentValidation request validation
- AutoMapper DTO mapping
- Global exception handling middleware
- Standardized API response structure
- Pagination & sorting support
- Static file serving (`wwwroot`)
- OpenAPI documentation
- Scalar API UI integration

---

# ⚙️ Tech Stack

| Technology | Description |
|------------|-------------|
| .NET 10 | ASP.NET Core Web API |
| Entity Framework Core 10 | ORM |
| PostgreSQL | Database |
| Npgsql | PostgreSQL EF Core provider |
| FluentValidation | Request validation |
| AutoMapper | DTO mapping |
| Scalar.AspNetCore | API UI |

---

# 🏗️ Project Structure

```text
MiniAppAPI/
├── MiniAppApi/
│   ├── Controllers/          # API endpoints
│   ├── Data/                 # DbContext, configurations, migrations
│   ├── Dtos/                 # Request & Response DTOs
│   ├── Middleware/           # Exception handling middleware
│   ├── Models/               # Domain models
│   ├── Profiles/             # AutoMapper profiles
│   ├── Services/             # Business logic
│   ├── Utils/                # Helper utilities
│   ├── Program.cs
│   └── ServiceRegistration.cs
└── MiniAppApi.slnx
```

---

# 📋 Requirements

Before running the project, make sure the following are installed:

- **.NET SDK 10.0+**
- **PostgreSQL Server**

Optional EF Core CLI tool:

```bash
dotnet tool install --global dotnet-ef
```

---

# ⚡ Configuration

The project uses:

```csharp
AddAppSettingsMultiPlatformJson(builder, "Mac")
```

Create the following file locally:

```text
MiniAppApi/appsettings.Mac.json
```

Example configuration:

```json
{
  "ConnectionStrings": {
    "PostgreSqlConnection": "Host=localhost;Port=5432;Database=miniappdb;Username=postgres;Password=your_password"
  }
}
```

> ⚠️ The connection string name must be `PostgreSqlConnection` because it is referenced inside `ServiceRegistration.cs`.

---

# 🚀 Installation & Running

Navigate to the repository root:

```bash
cd <repo-root>
```

Restore dependencies and build:

```bash
dotnet restore
dotnet build
```

Apply migrations:

```bash
dotnet ef database update --project MiniAppApi --startup-project MiniAppApi
```

Run the API:

```bash
dotnet run --project MiniAppApi
```

Default development URL:

```text
http://localhost:5075
```

---

# 📚 API Documentation

After starting the application:

| Service | URL |
|---------|-----|
| Scalar UI | `http://localhost:5075/scalar` |
| OpenAPI JSON | `http://localhost:5075/openapi/v1.json` |

---

# 🌐 Core Endpoints

Base route:

```text
/api
```

---

## 👥 Organizers

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Organizers` | Get organizers with pagination |
| POST | `/api/Organizers` | Create organizer |
| POST | `/api/Organizers/{id}/logo` | Upload organizer logo |
| GET | `/api/Organizers/{organizerId}/events` | Get organizer events |

---

## 🎉 Events

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Events` | Get events with pagination |
| POST | `/api/Events` | Create event |
| POST | `/api/Events/{id}/banner` | Upload event banner |
| GET | `/api/Events/{eventId}/tickets` | Get event tickets |
| GET | `/api/Events/{eventId}/organizer` | Get event organizer |
| POST | `/api/Events/{eventId}/tickets` | Create ticket for event |

---

## 🎫 Tickets

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Tickets` | Get tickets with pagination |
| POST | `/api/Tickets` | Create ticket |

---

# 📄 Pagination & Sorting

Supported query parameters (`PaginationParams`):

| Parameter | Default | Description |
|-----------|---------|-------------|
| PageNumber | `1` | Current page |
| PageSize | `10` | Items per page |
| SortBy | `Id` | Sorting field |
| Ascending | `true` | Sort direction |

Example:

```http
GET /api/Events?PageNumber=1&PageSize=10&SortBy=Date&Ascending=false
```

---

# 🧪 Request Examples

## Create Organizer

```http
POST /api/Organizers
Content-Type: application/json
```

```json
{
  "name": "Tech Conf Org",
  "email": "contact@techconf.az",
  "phone": "+994501112233"
}
```

---

## Create Ticket

```http
POST /api/Tickets
Content-Type: application/json
```

```json
{
  "type": "VIP",
  "price": 99.99,
  "eventId": 1
}
```

---

# 📦 Response Structure

All API responses follow the standardized `ApiResponse<T>` format:

```json
{
  "success": true,
  "message": "Operation successful",
  "data": {},
  "timestamp": "2026-01-01T12:00:00Z"
}
```

Paginated endpoints return:

```json
{
  "items": [],
  "totalCount": 0,
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 0,
  "hasNextPage": false,
  "hasPreviousPage": false
}
```

---

# 🛡️ Error Handling

Global middleware maps exceptions as follows:

| Exception | HTTP Status |
|-----------|-------------|
| EntityNotFoundException | 404 Not Found |
| ApplicationException | 400 Bad Request |
| Other Exceptions | 500 Internal Server Error |

Error responses also use the standardized `ApiResponse<object>` format.

---

# 📁 File Upload Notes

Uploaded files are stored under:

## Organizer Logos

```text
wwwroot/uploads/organizers/{organizerId}/
```

## Event Banners

```text
wwwroot/uploads/events/{eventId}/
```

Static files are served using:

```csharp
UseStaticFiles()
```

---

# 🛠️ Troubleshooting

## Connection String Issues

- Verify `appsettings.Mac.json`
- Ensure `PostgreSqlConnection` key exists

## Database Update Problems

- Make sure PostgreSQL server is running
- Verify credentials & permissions

## Port Differences

Check:

```text
launchSettings.json
```

for the current `applicationUrl`.

---

# 🔄 Development Flow

```bash
# 1) Build
dotnet build

# 2) Apply migrations
dotnet ef database update --project MiniAppApi --startup-project MiniAppApi

# 3) Run API
dotnet run --project MiniAppApi
```

---

# 🤝 Contributing

Contributions are welcome!

1. Fork the repository
2. Create a new feature branch
3. Commit your changes
4. Open a Pull Request

---

# 📄 License

This project is intended for educational and demonstration purposes.
