![Build and Test](https://github.com/michalkosiec/finance-tracker-api/actions/workflows/dotnet.yml/badge.svg)

# Finance Tracker API
A containerized REST API for personal finance management. Track incomes and expenses, organize transactions into categories, and define monthly budgets with strict validation rules.

Quick links
- Interactive API docs: [http://localhost:5200/scalar](http://localhost:5200/scalar)
- API base URL: `http://localhost:5200`

Tech stack
- Backend: .NET 10 (ASP.NET Core Web API)
- Database: PostgreSQL 15 (via Docker)
- Orchestration: Docker + Docker Compose
- API docs: Scalar / OpenAPI

Documentation
- Overview: [docs/OVERVIEW.md](docs/OVERVIEW.md)
- Setup: [docs/SETUP.md](docs/SETUP.md)
- API reference: [docs/API_REFERENCE.md](docs/API_REFERENCE.md)
- Architecture: [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md)
- Contributing: [CONTRIBUTING.md](CONTRIBUTING.md)

Prerequisites
- Docker and Docker Compose installed
- (Optional) .NET 10 SDK for local debugging

Quickstart (Docker)
1. Copy the example environment file:

```powershell
cp .env.example .env
```

2. Start services:

```powershell
docker-compose up --build
```

3. Open the API docs in your browser:

```text
http://localhost:5200/scalar
```

Notes
- Keycloak realm file is available in the `keycloak/` folder; the compose setup will expose an authentication server when enabled.
- Local DB (Postgres) is exposed at `localhost:5935` by the default compose setup.

Development
- To run the API locally without containers, open `src/FinanceTracker.Api` in your IDE and run the project with the .NET 10 SDK.

Contributing
- Open issues or PRs for documentation, bugs, or improvements.

License / Contact
- License: [LICENSE](LICENSE)
- Author / Maintainers: See repository metadata or package manifest
