using ProductService.DTOs;
using ProductService.Models;
using ProductService.Repositories;
namespace ProductService.Services
{
    public class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductServiceImpl(IProductRepository repo) { _repo = repo; }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync() =>
            (await _repo.GetAllAsync()).Select(Map);

        public async Task<IEnumerable<ProductDTO>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return await GetAllAsync();
            return (await _repo.SearchAsync(keyword)).Select(Map);
        }

        public async Task<IEnumerable<ProductDTO>> GetPagedAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;
            return (await _repo.GetPagedAsync(page, pageSize)).Select(Map);
        }

        public async Task<ProductDTO?> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p == null ? null : Map(p);
        }

        public async Task<ProductDTO> CreateAsync(CreateProductDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) throw new ArgumentException("Name is required.");
            if (dto.Price <= 0) throw new ArgumentException("Price must be > 0.");
            if (dto.Stock < 0) throw new ArgumentException("Stock cannot be negative.");
            var product = new Product { Name = dto.Name, Description = dto.Description, Price = dto.Price, ImageUrl = dto.ImageUrl, Stock = dto.Stock };
            var created = await _repo.AddAsync(product);
            return Map(created);
        }

        public async Task<ProductDTO?> UpdateAsync(int id, CreateProductDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) throw new ArgumentException("Name is required.");
            if (dto.Price <= 0) throw new ArgumentException("Price must be > 0.");
            var product = new Product { ProductId = id, Name = dto.Name, Description = dto.Description, Price = dto.Price, ImageUrl = dto.ImageUrl, Stock = dto.Stock };
            var ok = await _repo.UpdateAsync(product);
            return ok ? Map(product) : null;
        }

        public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);

        private static ProductDTO Map(Product p) =>
            new ProductDTO { ProductId = p.ProductId, Name = p.Name, Description = p.Description, Price = p.Price, ImageUrl = p.ImageUrl, Stock = p.Stock };
    }
}
