namespace task1_framework.Models;

public class CreateItemRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}