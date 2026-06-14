# Setup — Local development and Docker

Prerequisites
- Docker and Docker Compose
- (Optional) .NET 10 SDK for local debugging and running migrations

Docker quickstart
1. Copy environment template:

```powershell
cp .env.example .env
```

2. Build and start services:

```powershell
docker-compose up --build
```

3. Verify services:
- API docs: `http://localhost:5200/scalar`
- Postgres (default): `localhost:5935`

Running locally (without Docker)
1. Set local environment variables or copy `.env.example` values into user secrets.
2. From `src/FinanceTracker.Api` run:

```powershell
dotnet run --project src/FinanceTracker.Api/FinanceTracker.Api.csproj
```

Database migrations
- If using EF Core migrations locally, use the dotnet-ef tools to apply migrations.

Authentication (Keycloak)
- A Keycloak realm export is available in the `keycloak/` folder. The compose setup may include Keycloak depending on environment flags — adjust `docker-compose.yml` as needed.
