using ECommerce.API.DTOs;

namespace ECommerce.API.Services
{
    // Interface — defines the contract for product business logic
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int id);
        Task<ProductDTO> CreateProductAsync(CreateProductDTO dto);
        Task<ProductDTO?> UpdateProductAsync(int id, CreateProductDTO dto);
        Task<bool> DeleteProductAsync(int id);
    }
}
