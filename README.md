# Finance Tracker

A modern, containerized REST API for personal finance management.

Finance Tracker allows users to track incomes and expenses, organize transactions into custom categories, and set strict monthly budgets. The system is designed with a multi-tenant architecture to ensure full data isolation between users. It features an automated validation engine that enforces business rules — for example, automatically blocking new expenses that would exceed a user's predefined category limit.

## Getting Started

You don't need the .NET SDK or PostgreSQL installed on your machine. The entire ecosystem is fully containerized.

**1. Configure the environment**
Create your local configuration file by copying the provided template:

```bash
cp .env.example .env
```

_(Optional: Open `.env` to customize your database password or ports if needed)._

**2. Spin up the infrastructure**
Build the containers, run automatic database migrations, and launch the API with a single command:

```bash
docker-compose up --build
```

**3. Explore and test the API**
Once the services are up and running, you can interact with the API directly from your browser using the built-in Scalar interface:

- **Interactive API Reference:** [http://localhost:5200/scalar](http://localhost:5200/scalar)
- **Direct API Access:** `http://localhost:5200`
- **Local Database Access:** `localhost:5935` (for tools like DBeaver/pgAdmin)

---

## Tech Stack Overview

- **Backend:** .NET 10.0 (ASP.NET Core Web API)
- **Database:** PostgreSQL 15 + Entity Framework Core 10
- **Infrastructure:** Docker & Docker Compose (with automated transient migration container)
- **Frontend:** Swift / iOS (Planned)
