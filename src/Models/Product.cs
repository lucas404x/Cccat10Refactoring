namespace Cccat10RefactoringCode.Models;

public class Product 
{
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public decimal Subtotal { get; init; }

    public Product(string description, decimal price, int quantity) 
    {
        Description = description;
        Price = price;
        Quantity = quantity;
        Subtotal = Price * Quantity;
    }
}