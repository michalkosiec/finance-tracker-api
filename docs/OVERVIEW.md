# Finance Tracker API — Overview

Finance Tracker API is a containerized backend for personal finance tracking. It supports user accounts, categorized transactions, and monthly budgets with enforcement rules to prevent overspending.

Key points
- Multi-tenant data isolation at the database level.
- Validation engine enforces business rules (unique categories, budget checks, safe deletions).
- Exposes a REST API with OpenAPI/Scalar documentation.

This repo contains the API sources under `src/` and a Docker Compose setup for local development.
