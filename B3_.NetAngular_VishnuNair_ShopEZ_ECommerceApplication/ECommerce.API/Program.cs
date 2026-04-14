using ECommerce.API.Data;
using ECommerce.API.Repositories;
using ECommerce.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ─── 1. Register Services (Dependency Injection) ────────────────────────────

// Add controllers
builder.Services.AddControllers();

// Add EF Core with SQL Server
// Connection string is read from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repository (scoped = one instance per HTTP request)
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Register Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Add Swagger/OpenAPI for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ShopEZ Backend API",
        Version = "v1",
        Description = "E-Commerce Backend API for ShopEZ — Phase 2"
    });
});

// ─── 2. Allow CORS (so Phase-1 frontend can call these APIs) ────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ─── 3. Configure Middleware Pipeline ───────────────────────────────────────

// Enable Swagger UI in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopEZ API v1");
        c.RoutePrefix = string.Empty; // Swagger opens at root URL
    });
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// ─── 4. Auto-apply EF Core migrations on startup ────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate(); // Creates DB + tables if they don't exist
}

app.Run();
