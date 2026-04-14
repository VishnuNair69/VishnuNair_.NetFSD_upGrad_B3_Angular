namespace ECommerce.API.DTOs
{
    // ─── Product DTOs ───────────────────────────────────────────────────────────

    // Used when returning product data to the client (GET responses)
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
    }

    // Used when creating or updating a product (POST / PUT request body)
    public class CreateProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
    }

    // ─── Order DTOs ─────────────────────────────────────────────────────────────

    // Represents a single cart item sent from the frontend
    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    // POST /api/orders request body — the frontend sends UserId + list of cart items
    public class CreateOrderDTO
    {
        public int UserId { get; set; }
        public List<CartItemDTO> CartItems { get; set; } = new();
    }

    // Represents a single order item returned in the response
    public class OrderItemDTO
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    // Full order response sent back to the client
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new();
    }
}
