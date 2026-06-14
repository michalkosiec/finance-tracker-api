# Backend — Technical Overview

This document describes the backend architecture, core business rules, REST endpoints, and error mapping for the Finance Tracker API.

Architecture
- Clean Architecture style separation of concerns (project folders under `src/`):
  - `FinanceTracker.Domain` — Entities, value objects, and domain interfaces (e.g. `IUserOwned`, `IEntity`). Domain is persistence-agnostic.
  - `FinanceTracker.Application` — Use-cases, DTOs, MediatR features, and application services. Validation rules and business behaviors live here.
  - `FinanceTracker.Infrastructure` — Framework integrations and concrete implementations (EF Core persistence, Keycloak identity service, repository implementations).
  - `FinanceTracker.Api` — Presentation layer (ASP.NET Core controllers, request/response mapping, `AppControllerBase`).

Notes on user ownership
- Domain entities that belong to a user implement `IUserOwned` (contains a `UserId` property).
- Ownership enforcement is handled in the repository implementations or application services by filtering on `UserId` when querying data. There is no single `UserOwnedRepo` class in the codebase; instead, ownership checks follow the Clean Architecture separation between interfaces (Domain/Application) and implementations (Infrastructure).

Core business rules (validation highlights)
- Category names must be unique per user.
- Categories linked to active budgets cannot be deleted.
- Budgets cannot be lowered below already-recorded expenses for that month.
- Expense transactions that would exceed a budget are rejected by the validation engine.

Authentication
- Most endpoints require `Authorization: Bearer <JWT>`. Authentication flows are implemented in the `/auth` module.

API endpoints (summary)
- `POST /auth/register` — Register a new user (email must be unique).
- `POST /auth/login` — Login and receive a JWT (short-lived token).
- `GET /auth/me` — Current user profile.
- `GET|POST|PUT|DELETE /categories` — Manage user categories.
- `GET|POST|PUT|DELETE /budgets` — Define monthly category budgets (date format `yyyy-mm`).
- `GET|POST|PUT|DELETE /transactions` — Record and manage transactions (date `yyyy-mm-dd`, currency ISO code, type `income`/`expense`).
- `GET /stats/summary?month=yyyy-mm` — Monthly totals (income, expenses, balance).
- `GET /stats/by-category?month=yyyy-mm` — Expenses grouped by category.
- `GET /stats/monthly?year=yyyy` — Yearly overview (12 months).

Error mapping (middleware)
- `KeyNotFoundException` → `404 Not Found` (resource missing)
- `BadHttpRequestException` → `400 Bad Request` (business validation failure)
- `UnauthorizedAccessException` → `403 Forbidden` (forbidden resource access)
- other exceptions → `500 Internal Server Error` (unexpected failures)

See the source code in `src/` for controller signatures and DTO shapes for each endpoint.