# API Reference — Summary

Authentication
- `POST /auth/register` — Register (email must be unique)
- `POST /auth/login` — Login and receive a JWT
- `GET /auth/me` — Current user profile

Categories (`/categories`)
- `GET /categories`
- `GET /categories/{id}`
- `POST /categories`
- `PUT /categories/{id}`
- `DELETE /categories/{id}`

Budgets (`/budgets`)
- `GET /budgets`
- `GET /budgets/{id}`
- `POST /budgets` (month format `yyyy-mm`)
- `PUT /budgets/{id}`
- `DELETE /budgets/{id}`

Transactions (`/transactions`)
- `GET /transactions` — optional filters: `month`, `categoryid`, `type`
- `POST /transactions` — date `yyyy-mm-dd`, currency ISO code, `income`/`expense`
- `PUT /transactions/{id}`
- `DELETE /transactions/{id}`

Statistics (`/stats`)
- `GET /stats/summary?month=yyyy-mm`
- `GET /stats/by-category?month=yyyy-mm`
- `GET /stats/monthly?year=yyyy`

Notes
- All endpoints (except register/login) require `Authorization: Bearer <JWT>` header.
- Use the interactive Scalar/OpenAPI UI for request/response shapes: `http://localhost:5200/scalar`.

Examples

1) Register a new user

```bash
curl -X POST "http://localhost:5200/auth/register" \
	-H "Content-Type: application/json" \
	-d '{"name":"Alice","email":"alice@example.com","password":"P@ssw0rd"}'
```

2) Login and obtain a JWT

```bash
curl -X POST "http://localhost:5200/auth/login" \
	-H "Content-Type: application/json" \
	-d '{"email":"alice@example.com","password":"P@ssw0rd"}'

# Response contains a token you can reuse in `Authorization` header:
# { "token": "<JWT_TOKEN>", "expiresIn": 7200 }
```

3) Get categories (authorized request)

```bash
curl -X GET "http://localhost:5200/categories" \
	-H "Authorization: Bearer <JWT_TOKEN>"
```

