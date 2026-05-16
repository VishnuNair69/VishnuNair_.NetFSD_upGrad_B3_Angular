using Dapper;
using Microsoft.Data.SqlClient;
using ProductService.Models;

namespace ProductService.Repositories
{
    // ── Uses Dapper (lightweight ORM) instead of EF Core ──
    // Dapper maps SQL query results directly to C# objects — much faster for read-heavy operations
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        // Helper: create a new SQL connection
        private SqlConnection GetConnection() => new SqlConnection(_connectionString);

        // GET all products — raw SQL via Dapper
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var conn = GetConnection();
            const string sql = "SELECT ProductId, Name, Description, Price, ImageUrl, Stock FROM Products";
            return await conn.QueryAsync<Product>(sql);
        }

        // SEARCH by keyword — Dapper with parameterized query (safe, no SQL injection)
        public async Task<IEnumerable<Product>> SearchAsync(string keyword)
        {
            using var conn = GetConnection();
            const string sql = @"
                SELECT ProductId, Name, Description, Price, ImageUrl, Stock
                FROM Products
                WHERE Name LIKE @Keyword OR Description LIKE @Keyword";
            return await conn.QueryAsync<Product>(sql, new { Keyword = $"%{keyword}%" });
        }

        // PAGED listing — Dapper with OFFSET/FETCH for pagination
        public async Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize)
        {
            using var conn = GetConnection();
            const string sql = @"
                SELECT ProductId, Name, Description, Price, ImageUrl, Stock
                FROM Products
                ORDER BY ProductId
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            return await conn.QueryAsync<Product>(sql, new { Offset = (page - 1) * pageSize, PageSize = pageSize });
        }

        // GET by ID
        public async Task<Product?> GetByIdAsync(int id)
        {
            using var conn = GetConnection();
            const string sql = "SELECT * FROM Products WHERE ProductId = @Id";
            return await conn.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
        }

        // INSERT new product
        public async Task<Product> AddAsync(Product product)
        {
            using var conn = GetConnection();
            const string sql = @"
                INSERT INTO Products (Name, Description, Price, ImageUrl, Stock)
                VALUES (@Name, @Description, @Price, @ImageUrl, @Stock);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";
            var newId = await conn.ExecuteScalarAsync<int>(sql, product);
            product.ProductId = newId;
            return product;
        }

        // UPDATE product
        public async Task<bool> UpdateAsync(Product product)
        {
            using var conn = GetConnection();
            const string sql = @"
                UPDATE Products SET
                    Name = @Name, Description = @Description,
                    Price = @Price, ImageUrl = @ImageUrl, Stock = @Stock
                WHERE ProductId = @ProductId";
            var rows = await conn.ExecuteAsync(sql, product);
            return rows > 0;
        }

        // DELETE product
        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = GetConnection();
            var rows = await conn.ExecuteAsync("DELETE FROM Products WHERE ProductId = @Id", new { Id = id });
            return rows > 0;
        }
    }
}
