![Build and Test](https://github.com/michalkosiec/finance-tracker-api/actions/workflows/dotnet.yml/badge.svg)

# Finance Tracker API

A professional, containerized REST API for personal finance management.

Finance Tracker API provides a robust backend system designed to track incomes and expenses, organize transactions into custom categories, and manage strict monthly budgets. The architecture enforces multi-tenancy at the database level, ensuring full data isolation between users. It features an automated validation engine that guarantees data integrity and applies complex financial rules—such as blocking expenses that exceed predefined category limits.

## Tech Stack Overview

- **Backend:** .NET 10.0 (ASP.NET Core Web API)
- **Database:** PostgreSQL 15 + Entity Framework Core 10
- **Infrastructure:** Docker & Docker Compose
- **API Documentation:** Scalar (OpenAPI integration)

---

## Getting Started

You do **not** need the .NET SDK or a local instance of PostgreSQL installed on your machine. The entire infrastructure is fully containerized and ready to run.

### 1. Configure the environment

Create your local configuration file by copying the provided template:

```bash
cp .env.example .env
```

_(Optional: Open the `.env` file to customize your database credentials or ports)._

### 2. Spin up the infrastructure

Build the containers, execute automatic Entity Framework database migrations, and launch the API server with a single command:

```bash
docker-compose up --build
```

### 3. Explore and test the API

Once the Docker containers are successfully up and running, you can easily interact with the API endpoints directly from your browser using the built-in Scalar interface:

- **Interactive API Reference:** [http://localhost:5200/scalar](http://localhost:5200/scalar)
- **Direct API Access:** `http://localhost:5200`
- **Local Database Access:** `localhost:5935` (Connect using tools like DBeaver or pgAdmin)
