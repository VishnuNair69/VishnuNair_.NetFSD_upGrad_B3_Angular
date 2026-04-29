using DemoService.Models;
using System.Text.Json;

namespace DemoService.Services
{
    public class ProductApiService : IProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProducts()
        {
            var response = await _httpClient.GetAsync($"https://localhost:7022/api/Product/");

            var jsonData = await response.Content.ReadAsStringAsync();

            // It converts JSON strings into C# objects
            // Use JsonSerializer.Deserialize<T>(jsonString) for direct conversion

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            var productData = JsonSerializer.Deserialize<List<Product>>(jsonData, options);

            return productData;
        }
        public async Task<Product> GetProductId(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7200/api/Product/{id}");


            var jsonData = await response.Content.ReadAsStringAsync();


            // It converts JSON strings into C# objects
            // Use JsonSerializer.Deserialize<T>(jsonString) for direct conversion

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            var productData = await response.Content.ReadFromJsonAsync<Product>(options);

            return productData;
        }
        public async Task<Product> AddProduct(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7200/api/Product", product);

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            // returns result as json format
            var jsonData = await response.Content.ReadFromJsonAsync<Product>(options);

            return jsonData;
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7022/api/Product/{id}", product);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var updatedProduct = await response.Content.ReadFromJsonAsync<Product>(options);

            return updatedProduct;
        }


        public async Task<string> DeleteProduct(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7022/api/Product/{id}");

            return await response.Content.ReadAsStringAsync();
        }
    }
}
