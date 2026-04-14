using ECommerce.API.DTOs;
using ECommerce.API.Models;
using ECommerce.API.Repositories;

namespace ECommerce.API.Services
{
    // Contains product business logic — sits between Controller and Repository
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Fetch all products and map to DTO
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            // LINQ projection: convert each Product entity → ProductDTO
            return products.Select(p => MapToDTO(p));
        }

        // Fetch single product by ID
        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;
            return MapToDTO(product);
        }

        // Create new product after validating input
        public async Task<ProductDTO> CreateProductAsync(CreateProductDTO dto)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Product name cannot be empty.");

            if (dto.Price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            if (dto.Stock < 0)
                throw new ArgumentException("Stock cannot be negative.");

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                Stock = dto.Stock
            };

            var created = await _productRepository.AddAsync(product);
            return MapToDTO(created);
        }

        // Update an existing product
        public async Task<ProductDTO?> UpdateProductAsync(int id, CreateProductDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Product name cannot be empty.");

            if (dto.Price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            var updated = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                Stock = dto.Stock
            };

            var result = await _productRepository.UpdateAsync(id, updated);
            if (result == null) return null;

            return MapToDTO(result);
        }

        // Delete a product
        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        // ─── Helper: map Product entity → ProductDTO ────────────────────────────
        private static ProductDTO MapToDTO(Product p)
        {
            return new ProductDTO
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Stock = p.Stock
            };
        }
    }
}
