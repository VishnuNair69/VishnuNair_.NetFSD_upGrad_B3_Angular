namespace OrderService.DTOs
{
    public class CartItemDTO { public int ProductId { get; set; } public string ProductName { get; set; } = ""; public int Quantity { get; set; } public decimal Price { get; set; } }
    public class CreateOrderDTO { public int UserId { get; set; } public List<CartItemDTO> CartItems { get; set; } = new(); }
    public class OrderItemDTO { public int OrderItemId { get; set; } public int ProductId { get; set; } public string ProductName { get; set; } = ""; public int Quantity { get; set; } public decimal Price { get; set; } }
    public class OrderDTO { public int OrderId { get; set; } public int UserId { get; set; } public DateTime OrderDate { get; set; } public decimal TotalAmount { get; set; } public List<OrderItemDTO> Items { get; set; } = new(); }
}
