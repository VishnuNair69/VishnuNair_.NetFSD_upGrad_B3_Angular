using Microsoft.AspNetCore.Mvc;
using ProductService.DTOs;
using ProductService.Services;
namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
        {
            if (!string.IsNullOrWhiteSpace(search)) return Ok(await _service.SearchAsync(search));
            if (page > 0) return Ok(await _service.GetPagedAsync(page, pageSize));
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _service.GetByIdAsync(id);
            return p == null ? NotFound(new { message = $"Product {id} not found." }) : Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDTO dto)
        {
            try { var p = await _service.CreateAsync(dto); return StatusCode(201, p); }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProductDTO dto)
        {
            try
            {
                var p = await _service.UpdateAsync(id, dto);
                return p == null ? NotFound(new { message = $"Product {id} not found." }) : Ok(p);
            }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound(new { message = $"Product {id} not found." });
        }
    }
}
