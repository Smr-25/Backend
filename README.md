# Backend Learning Archive

A structured archive of ASP.NET Core and .NET backend coursework. The repository contains one lab and a set of independent homework projects covering MVC, Web APIs, Razor Pages, data access, layered architecture, caching, and messaging.

The original application code, migrations, project files, and static assets are preserved. This organization changes repository paths and documentation, not the behavior or scope of the projects. In particular, Pustok remains at its original early stage.

## At a glance

| | Count |
|---|---:|
| Labs | 1 |
| Backend homework project groups | 10 |
| Companion frontend folders | 1 |
| .NET project files | 18 |

Projects target either `.NET 8` or `.NET 10`; check the target shown below before opening or running one.

## Repository structure

```text
Backend/
├── Labs/
│   └── MiniAppAPI/
├── Homework/
│   ├── EternaApp/
│   ├── FirstApiApp/
│   ├── MentorApp/
│   ├── MenuApp/
│   ├── MenuAppFront/
│   ├── MessageBrokersMQ/
│   ├── OnionArchApp/
│   ├── PustokApp/
│   ├── RazorPagesApp/
│   ├── SendToDataView/
│   └── TagHelpersExample/
├── .gitignore
└── README.md
```

`MiniAppAPI` is the lab. Everything under `Homework` is homework; `MenuAppFront` is a small browser UI kept alongside the backend projects it accompanies. Each .NET solution remains inside its own project folder, so projects can be opened independently.

## Lab

| Project | Framework | Focus |
|---|---|---|
| [MiniAppAPI](Labs/MiniAppAPI/) | .NET 10 | Event, organizer, and ticket API with PostgreSQL, EF Core, DTOs, validation, pagination, file uploads, and centralized error handling |

MiniAppAPI exposes controllers under `/api/Organizers`, `/api/Events`, and `/api/Tickets`. Its local launch profile uses `http://localhost:5075`, with OpenAPI at `/openapi/v1.json` and Scalar UI at `/scalar`. The URL can change if the launch profile is edited.

The lab reads its PostgreSQL connection string from the `PostgreSqlConnection` key. On macOS, the project also loads a local `MiniAppApi/appsettings.Mac.json` file; keep real credentials there or in another local configuration source, not in Git. The existing migrations, sample uploads, and API request file remain with the project.

To run the lab after configuring PostgreSQL:

```bash
dotnet run --project Labs/MiniAppAPI/MiniAppApi/MiniAppApi.csproj
```

## Homework

| Project | Framework | Focus |
|---|---|---|
| [EternaApp](Homework/EternaApp/) | .NET 8 | MVC pages, EF Core data, services, view models, and portfolio content |
| [FirstApiApp](Homework/FirstApiApp/) | .NET 10 | Web API, categories and products, JWT authentication, validation, file handling, and a consuming web app |
| [MentorApp](Homework/MentorApp/) | .NET 8 | MVC pages backed by pricing, service, slider, and other content models |
| [MenuApp](Homework/MenuApp/) | .NET 10 | Web API with authentication, Redis integration, and a Hangfire product-status job |
| [MessageBrokersMQ](Homework/MessageBrokersMQ/) | .NET 10 | Console examples with and without a RabbitMQ producer/consumer |
| [OnionArchApp](Homework/OnionArchApp/) | .NET 10 | Layered Web API with domain, application, persistence, infrastructure, and presentation projects |
| [PustokApp](Homework/PustokApp/) | .NET 10 | Early-stage bookstore MVC application with book data, images, and shared layouts |
| [RazorPagesApp](Homework/RazorPagesApp/) | .NET 10 | Razor Pages for categories and products with EF Core |
| [SendToDataView](Homework/SendToDataView/) | .NET 8 | Passing model data from MVC controllers to views |
| [TagHelpersExample](Homework/TagHelpersExample/) | .NET 8 | MVC Tag Helper examples built around car and model pages |

[MenuAppFront](Homework/MenuAppFront/) contains HTML, CSS, and JavaScript pages related to the MenuApp exercise. It is a companion UI, not another .NET backend project.

## Topics covered

- ASP.NET Core MVC, Razor Pages, and REST-style Web APIs
- Controllers, model binding, view models, DTOs, and validation
- Entity Framework Core, migrations, and relational databases
- Authentication and authorization in the API exercises
- Service layers, dependency injection, and Onion Architecture
- Redis caching, Hangfire jobs, and RabbitMQ messaging
- Static assets, file uploads, and browser-facing companion pages

These topics appear in different exercises; no single project implements every item.

## Running a project

Use the SDK matching that project's target framework, then open its `.sln` or `.slnx` file in your editor. An individual project can also be started with:

```bash
dotnet run --project path/to/Project.csproj
```

Most data-backed homework projects require a local SQL Server connection. MiniAppAPI uses PostgreSQL; MenuApp also demonstrates Redis and Hangfire; the message-broker exercises require RabbitMQ for the producer/consumer flow. Set up only the services needed by the project you choose to run.

Several projects load platform-specific local configuration such as `appsettings.Mac.json`. The root `.gitignore` excludes these local files, IDE state, build output, and generated database artifacts. Do not commit real passwords, tokens, or connection strings.

## Notes

- Folder paths were standardized, but internal namespaces, project names, application logic, EF Core migrations, and existing assets were not rewritten.
- Pustok was not expanded to match a later or larger version; its current learning-stage implementation is intentionally retained.
- This organization was checked structurally. No claim is made that every project builds or runs without its own dependencies and local configuration.
- The repository is maintained for educational and portfolio review.
