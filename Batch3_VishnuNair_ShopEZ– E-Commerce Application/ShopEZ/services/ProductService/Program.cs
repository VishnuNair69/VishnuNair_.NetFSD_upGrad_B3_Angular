using ProductService.Repositories;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "Product Service (Dapper)", Version = "v1" }));

// Note: No EF Core here — using Dapper directly with IConfiguration for connection string
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductServiceImpl>();
builder.Services.AddCors(o => o.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Service v1"); c.RoutePrefix = string.Empty; });
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
