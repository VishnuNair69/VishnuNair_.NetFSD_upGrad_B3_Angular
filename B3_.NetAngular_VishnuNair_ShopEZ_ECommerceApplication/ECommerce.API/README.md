# ShopEZ Backend API — Phase 2

ASP.NET Core Web API backend for the ShopEZ e-commerce application.  
Built using **ASP.NET Core 8**, **Entity Framework Core**, and **SQL Server**.

---

## Project Structure

```
ECommerce.API/
├── Controllers/
│   ├── ProductsController.cs       ← Handles Product HTTP requests
│   └── OrdersController.cs         ← Handles Order HTTP requests
├── Models/
│   ├── User.cs
│   ├── Product.cs
│   ├── Order.cs
│   └── OrderItem.cs
├── DTOs/
│   └── DTOs.cs                     ← All request/response DTOs
├── Data/
│   └── ApplicationDbContext.cs     ← EF Core DbContext
├── Repositories/
│   ├── IProductRepository.cs
│   └── ProductRepository.cs
├── Services/
│   ├── IProductService.cs
│   ├── ProductService.cs
│   ├── IOrderService.cs
│   └── OrderService.cs
├── Migrations/                     ← EF Core migrations
├── appsettings.json                ← Connection string config
├── Program.cs                      ← App entry point + DI setup
└── ECommerce.API.csproj
```

---

## Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/) (or VS Code with C# extension)
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB ships with Visual Studio — no separate install needed)
- [Postman](https://www.postman.com/) for API testing

---

## How to Run

### Step 1 — Clone / Open the project

Open `ECommerce.API.csproj` in Visual Studio, or run:

```bash
cd ECommerce.API
```

### Step 2 — Check the connection string

Open `appsettings.json` and verify the connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ShopEZDb;Trusted_Connection=True;"
}
```

If you're using a full SQL Server instance, change it to:

```
Server=YOUR_SERVER_NAME;Database=ShopEZDb;Trusted_Connection=True;
```

### Step 3 — Apply EF Core Migrations

The app auto-applies migrations on startup (`db.Database.Migrate()` in `Program.cs`).  
Or run manually via Package Manager Console in Visual Studio:

```bash
Update-Database
```

Or via CLI:

```bash
dotnet ef database update
```

### Step 4 — Run the project

Press **F5** in Visual Studio, or:

```bash
dotnet run
```

The API starts at `https://localhost:7xxx` (port shown in terminal).

### Step 5 — Open Swagger

Navigate to `https://localhost:7xxx` in your browser.  
Swagger UI opens automatically (configured as root URL).

---

## API Endpoints

### Products

| Method | Endpoint              | Description           |
|--------|-----------------------|-----------------------|
| GET    | `/api/products`       | Get all products      |
| GET    | `/api/products/{id}`  | Get product by ID     |
| POST   | `/api/products`       | Create new product    |
| PUT    | `/api/products/{id}`  | Update product        |
| DELETE | `/api/products/{id}`  | Delete product        |

**POST / PUT Request Body:**
```json
{
  "name": "Gaming Mouse",
  "description": "High DPI wireless mouse",
  "price": 2499.00,
  "imageUrl": "/images/mouse.jpg",
  "stock": 25
}
```

---

### Orders

| Method | Endpoint            | Description                   |
|--------|---------------------|-------------------------------|
| POST   | `/api/orders`       | Create order from cart items  |
| GET    | `/api/orders`       | Get all orders                |
| GET    | `/api/orders/{id}`  | Get order by ID               |

**POST /api/orders Request Body:**
```json
{
  "userId": 1,
  "cartItems": [
    { "productId": 1, "quantity": 2 },
    { "productId": 3, "quantity": 1 }
  ]
}
```

**Response:**
```json
{
  "orderId": 1,
  "userId": 1,
  "orderDate": "2024-01-15T10:30:00Z",
  "totalAmount": 151800.00,
  "items": [
    {
      "orderItemId": 1,
      "productId": 1,
      "productName": "Laptop Pro X",
      "quantity": 2,
      "price": 75000.00
    },
    {
      "orderItemId": 2,
      "productId": 3,
      "productName": "USB-C Hub",
      "quantity": 1,
      "price": 1800.00
    }
  ]
}
```

---

## Architecture

```
HTTP Request
    ↓
Controller         (handles routing, request/response)
    ↓
Service            (business logic, validation, LINQ calculations)
    ↓
Repository         (data access — wraps EF Core operations)
    ↓
DbContext          (EF Core — talks to SQL Server)
    ↓
SQL Server Database
```

---

## Postman Testing

1. Import a new request in Postman
2. Set base URL to `https://localhost:YOUR_PORT`
3. Test endpoints in this order:
   - GET `/api/products` — confirm seed data loaded
   - POST `/api/products` — add a product
   - PUT `/api/products/1` — update it
   - POST `/api/orders` — place an order using productId from above
   - GET `/api/orders` — confirm order was saved
   - DELETE `/api/products/2` — delete a product

---

## Seeded Data

Three products are seeded automatically on first run:

| ID | Name                | Price    | Stock |
|----|---------------------|----------|-------|
| 1  | Laptop Pro X        | ₹75,000  | 15    |
| 2  | Wireless Headphones | ₹3,500   | 30    |
| 3  | USB-C Hub           | ₹1,800   | 50    |

---

## Troubleshooting

| Issue | Fix |
|-------|-----|
| DB not created | Run `dotnet ef database update` or check connection string |
| Port conflict | Change port in `launchSettings.json` |
| Migration error | Delete `Migrations/` folder and re-run `dotnet ef migrations add InitialCreate` |
| CORS error from frontend | Already configured — `AllowAll` policy is active in development |
