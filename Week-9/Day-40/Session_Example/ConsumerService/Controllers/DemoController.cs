using DemoService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoService.Services;

namespace DemoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IProductApiService _productApiService;

        public DemoController(IProductApiService productApiService)
        {
            _productApiService = productApiService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            return Ok(await _productApiService.GetProducts());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _productApiService.GetProductId(id));
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            return Ok(await _productApiService.AddProduct(product));
        }
    }
}
