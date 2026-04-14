using ECommerce.API.DTOs;
using ECommerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        // Dependency Injection — service injected via constructor
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET /api/products — Get all products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET /api/products/{id} — Get product by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound(new { message = $"Product with ID {id} not found." });

            return Ok(product);
        }

        // POST /api/products — Add new product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Request body cannot be null." });

            try
            {
                var created = await _productService.CreateProductAsync(dto);
                // Returns 201 Created with location header
                return CreatedAtAction(nameof(GetProduct), new { id = created.ProductId }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        // PUT /api/products/{id} — Update product
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateProductDTO dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Request body cannot be null." });

            try
            {
                var updated = await _productService.UpdateProductAsync(id, dto);
                if (updated == null)
                    return NotFound(new { message = $"Product with ID {id} not found." });

                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        // DELETE /api/products/{id} — Delete product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var deleted = await _productService.DeleteProductAsync(id);
                if (!deleted)
                    return NotFound(new { message = $"Product with ID {id} not found." });

                return NoContent(); // 204 No Content — standard delete success
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }
    }
}
