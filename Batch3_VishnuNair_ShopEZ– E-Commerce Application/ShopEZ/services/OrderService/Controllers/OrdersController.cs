using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.Services;
namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrdersController(IOrderService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var o = await _service.GetByIdAsync(id);
            return o == null ? NotFound(new { message = $"Order {id} not found." }) : Ok(o);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDTO dto)
        {
            try { var o = await _service.CreateAsync(dto); return StatusCode(201, o); }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        }
    }
}
