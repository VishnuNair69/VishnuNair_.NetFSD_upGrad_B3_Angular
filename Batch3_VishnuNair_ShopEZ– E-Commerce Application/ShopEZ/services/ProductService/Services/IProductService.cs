using ProductService.DTOs;
namespace ProductService.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<IEnumerable<ProductDTO>> SearchAsync(string keyword);
        Task<IEnumerable<ProductDTO>> GetPagedAsync(int page, int pageSize);
        Task<ProductDTO?> GetByIdAsync(int id);
        Task<ProductDTO> CreateAsync(CreateProductDTO dto);
        Task<ProductDTO?> UpdateAsync(int id, CreateProductDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
