# ShopEZ — Phase 4: Microservices + Docker

## Architecture

```
Angular SPA (http://localhost:4200)
         ↓
API Gateway — Ocelot (http://localhost:8080)
         ↓
┌──────────────┬──────────────┬──────────────┬──────────────┐
│ UserService  │ProductService│ OrderService │ CartService  │
│  port 8081   │  port 8082   │  port 8083   │  port 8084   │
│  EF Core     │   DAPPER     │  EF Core     │  EF Core     │
└──────────────┴──────────────┴──────────────┴──────────────┘
         ↓
SQL Server (port 1433) — 4 separate databases
```

## Services & Ports

| Service | Port | DB | ORM |
|---------|------|----|-----|
| API Gateway | 8080 | — | Ocelot |
| User Service | 8081 | ShopEZ_Users | EF Core |
| Product Service | 8082 | ShopEZ_Products | Dapper |
| Order Service | 8083 | ShopEZ_Orders | EF Core |
| Cart Service | 8084 | ShopEZ_Cart | EF Core |

## How to Run (Docker)

### Prerequisites
- Docker Desktop — https://www.docker.com/products/docker-desktop
- (Docker Desktop includes docker-compose)

### Step 1 — Start everything
Open terminal in the ShopEZ folder:
```bash
docker-compose up --build
```
First run takes 5-10 minutes (downloads images, builds services).

### Step 2 — Initialize Products table
After services are up, run the SQL script:
```bash
docker exec -it shopez-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "ShopEZ@123!" -i /dev/stdin < sql/init.sql
```

### Step 3 — Test via Swagger
- User Service:    http://localhost:8081
- Product Service: http://localhost:8082
- Order Service:   http://localhost:8083
- Cart Service:    http://localhost:8084
- API Gateway:     http://localhost:8080

### Step 4 — Stop everything
```bash
docker-compose down
```

## API Gateway Routes (Ocelot)

| Frontend calls | Gateway forwards to |
|---------------|---------------------|
| /gateway/users/register | User Service /api/users/register |
| /gateway/users/login | User Service /api/users/login |
| /gateway/products | Product Service /api/products |
| /gateway/orders | Order Service /api/orders |
| /gateway/cart/{userId} | Cart Service /api/cart/{userId} |

## Running Unit Tests

```bash
cd tests/UserService.Tests && dotnet test
cd tests/ProductService.Tests && dotnet test
cd tests/OrderService.Tests && dotnet test
cd tests/CartService.Tests && dotnet test
```

## Project Structure

```
ShopEZ/
├── services/
│   ├── UserService/       → Register, Login (port 8081)
│   ├── ProductService/    → CRUD + Search + Pagination using Dapper (port 8082)
│   ├── OrderService/      → Order creation + history (port 8083)
│   └── CartService/       → Cart add/remove/clear (port 8084)
├── ApiGateway/            → Ocelot routes all requests (port 8080)
├── tests/
│   ├── UserService.Tests/    → 8 xUnit tests
│   ├── ProductService.Tests/ → 8 xUnit tests
│   ├── OrderService.Tests/   → 8 xUnit tests
│   └── CartService.Tests/    → 7 xUnit tests
├── sql/init.sql           → DB seed script
└── docker-compose.yml     → Orchestrates all services
```
