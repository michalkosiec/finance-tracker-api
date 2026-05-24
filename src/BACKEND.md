# Backend API — Technical Specification

This documentation describes the technical architecture and REST API endpoints of the Finance Tracker system. The backend was designed following Clean Code principles and strict data isolation for individual users at the database level.

## Code Architecture

The application implements a layered separation of concerns:

1. **Controllers Layer (controllers/):** Responsible exclusively for authorization, accepting HTTP requests, and returning correct response codes. All user resources inherit from `AppControllerBase`, which automatically extracts the `UserId` from the logged-in user's JWT token.
2. **Repositories Layer (repositories/):** Implements the generic Repository pattern. The `UserOwnedRepo` class enforces on-the-fly that database queries automatically append the condition `.Where(e => e.UserId == userId)`, preventing leakage or modification of other users' data.
3. **Business Validation Layer (services/validation_service):** A dedicated service for verifying advanced financial logic rules that cannot be validated using standard model annotations.
4. **Global Exception Handler (middleware/exception_middleware):** Intercepts application errors at the pipeline level and transforms them into structured JSON responses with appropriate HTTP status codes.

---

## Key Business Rules (Validation Engine)

The application autonomously maintains data integrity using the `ValidationService`:

- **Category Uniqueness:** A user cannot create two categories with the exact same name.
- **Safe Deletion:** A category cannot be deleted if it is currently linked to any active budget.
- **Budget Expenditure Integrity:** The system will not allow setting a budget limit for a category to an amount lower than what the user has already physically spent in that given month.
- **Budget Overage Block:** When attempting to add a new Expense transaction, the system checks the sum of expenses for that month. If the new amount causes the defined budget limit to be exceeded, the transaction is immediately blocked and rejected.

---

## API Endpoints Overview

All endpoints (except registration and login) require an authorization header: `Authorization: Bearer <TOKEN_JWT>`.

### 1. Authentication Module (/auth)

- `POST /auth/register` — Registers a new user. Requires a unique email address; the password is automatically hashed before saving.
- `POST /auth/login` — Logs into the system. Verifies credentials using BCrypt and asynchronously generates a secure JWT token valid for 2 hours.
- `GET /auth/me` — Returns the profile of the currently logged-in user (Id, Name, Email) based on the provided token.

### 2. Categories Module (/categories)

Users manage their dictionary of expense/income categories (each category has name, icon, and color fields).

- `GET /categories` — Retrieves a list of all categories belonging to the logged-in user.
- `GET /categories/{id}` — Retrieves the details of a specific category.
- `POST /categories` — Creates a new category after verifying name uniqueness.
- `PUT /categories/{id}` — Updates the data of a specific category.
- `DELETE /categories/{id}` — Deletes a category (blocked if linked to budgets).

### 3. Budgets Module (/budgets)

Allows defining expense limits for specific categories on a monthly basis (date format: yyyy-mm).

- `GET /budgets` — Retrieves a list of all defined budgets.
- `GET /budgets/{id}` — Retrieves a budget by its ID.
- `POST /budgets` — Sets a new budget limit for a category for a given month.
- `PUT /budgets/{id}` — Updates the limit amount or the assigned category.
- `DELETE /budgets/{id}` — Deletes a budget definition.

### 4. Transactions Module (/transactions)

Registry of financial operations. A transaction has an amount, currency (ISO standard, e.g., PLN), date (yyyy-mm-dd), and type (income / expense).

- `GET /transactions` — Retrieves user transactions with optional Query String filtering by parameters: month, categoryid, and type.
- `POST /transactions` — Adds a new transaction (triggers budget limit validation for expenses).
- `PUT /transactions/{id}` — Modifies an existing transaction and recalculates limits.
- `DELETE /transactions/{id}` — Deletes a transaction from history.

### 5. Analytics and Statistics Module (/stats)

- `GET /stats/summary?month=yyyy-mm` — Returns a financial summary for the month: total income, total expenses, and the final balance (Income - Expenses).
- `GET /stats/by-category?month=yyyy-mm` — Returns a breakdown of expenses grouped by category, including the total amount and number of transactions for each.
- `GET /stats/monthly?year=yyyy` — Generates a full-year overview, returning an array of 12 months, where the total income and expenses are calculated for each month (useful for generating yearly charts).

---

## Exception Mapping to HTTP Codes (Middleware)

The application eliminates the need to write try-catch blocks in controllers. A global middleware component maps thrown business exceptions to standard network response codes:

| Exception thrown in code    | HTTP Response Code            | Reason / Business Scenario                                                                   |
| :-------------------------- | :---------------------------- | :------------------------------------------------------------------------------------------- |
| KeyNotFoundException        | **404 Not Found** | The specified category or transaction does not exist in the database.                        |
| BadHttpRequestException     | **400 Bad Request** | Business rule violation (category duplicate, budget overage, limit set too low).             |
| UnauthorizedAccessException | **403 Forbidden** | Attempt to modify or delete a resource belonging to another user (insufficient permissions). |
| _All other errors_          | **500 Internal Server Error** | An unexpected application error (automatically recorded in system logs).                     |