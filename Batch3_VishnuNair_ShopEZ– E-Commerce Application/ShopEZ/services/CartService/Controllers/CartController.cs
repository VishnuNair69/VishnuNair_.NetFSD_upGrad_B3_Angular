using CartService.DTOs;
using CartService.Services;
using Microsoft.AspNetCore.Mvc;
namespace CartService.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;
        public CartController(ICartService service) { _service = service; }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId) => Ok(await _service.GetCartAsync(userId));

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO dto)
        {
            try { return StatusCode(201, await _service.AddToCartAsync(dto)); }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            var ok = await _service.RemoveFromCartAsync(cartItemId);
            return ok ? NoContent() : NotFound(new { message = "Cart item not found." });
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> Clear(int userId) { await _service.ClearCartAsync(userId); return NoContent(); }
    }
}
