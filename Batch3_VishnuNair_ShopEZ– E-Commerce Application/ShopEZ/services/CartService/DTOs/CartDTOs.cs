namespace CartService.DTOs
{
    public class CartItemDTO { public int CartItemId { get; set; } public int UserId { get; set; } public int ProductId { get; set; } public string ProductName { get; set; } = ""; public decimal Price { get; set; } public int Quantity { get; set; } }
    public class AddToCartDTO { public int UserId { get; set; } public int ProductId { get; set; } public string ProductName { get; set; } = ""; public decimal Price { get; set; } public int Quantity { get; set; } }
}
