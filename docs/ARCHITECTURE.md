# Architecture — High level

Layers (Clean Architecture)
- `FinanceTracker.Domain` — Domain entities, value objects, and interfaces (e.g. `IUserOwned`, `IEntity`). No framework code here.
- `FinanceTracker.Application` — Application logic, use-cases, DTOs, and validation behaviors. This layer orchestrates domain rules.
- `FinanceTracker.Infrastructure` — Concrete implementations: EF Core persistence, repository implementations, Keycloak integration, and other framework code.
- `FinanceTracker.Api` — Presentation layer: ASP.NET Core controllers, routing, and API surface. Controllers inherit from `AppControllerBase` to access `UserId` from JWT.

Ownership enforcement
- Entities that belong to a user implement `IUserOwned`.
- Application services enforce ownership by filtering on the `UserId` value.

Validation and business rules
- Category uniqueness per user
- Safe deletion of categories linked to budgets
- Budget limits cannot be set below already-recorded monthly expenses
- Expenses that would exceed a budget are rejected

Error handling
- Business exceptions are mapped to appropriate HTTP codes by middleware. See `src/` for the exception-to-status mapping.
