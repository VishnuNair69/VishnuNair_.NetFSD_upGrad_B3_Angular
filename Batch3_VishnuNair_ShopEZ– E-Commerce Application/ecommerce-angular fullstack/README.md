# ShopEZ Angular Frontend — Phase 3

Angular SPA frontend for ShopEZ e-commerce application.
Connects to the ASP.NET Core backend (Phase 2).

## Prerequisites
- Node.js 18+ — https://nodejs.org
- Angular CLI: `npm install -g @angular/cli`
- Backend (ECommerce.API) must be running on https://localhost:7001

## Setup & Run

```bash
cd ecommerce-angular
npm install
ng serve
```

Open: http://localhost:4200

## Features
- Product catalog with search
- Product details page
- Shopping cart (with quantity control)
- Checkout + order placement
- User registration & login
- Admin panel (Add/Edit/Delete products, View orders)
- Role-based access (Admin vs Customer)

## API Base URL
Edit `src/app/services/product.service.ts` if your backend runs on a different port:
```ts
private apiUrl = 'https://localhost:7001/api/products';
```

## App Structure
```
src/app/
├── components/
│   ├── navbar/          ← Top navigation bar
│   ├── product-list/    ← Home page product grid
│   ├── product-details/ ← Single product view
│   ├── cart/            ← Shopping cart
│   ├── checkout/        ← Order placement
│   └── admin/           ← Admin product & order management
├── pages/
│   ├── login/           ← Login page
│   └── register/        ← Registration page
├── services/
│   ├── product.service.ts  ← Calls /api/products
│   ├── order.service.ts    ← Calls /api/orders
│   ├── cart.service.ts     ← Local cart state (BehaviorSubject)
│   └── auth.service.ts     ← Login/logout/user state
├── models/              ← TypeScript interfaces matching backend DTOs
├── guards/              ← Route protection (authGuard, adminGuard)
├── app.routes.ts        ← All routes with lazy loading
└── app.config.ts        ← App-level providers (HttpClient, Router)
```

## Testing
1. Register as Admin → go to Admin panel → add products
2. Logout → Register as Customer
3. Browse products → Add to cart → Checkout
4. Login as Admin → View orders
