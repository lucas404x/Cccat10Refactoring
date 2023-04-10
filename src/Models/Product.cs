namespace Cccat10RefactoringCode.Models;

public class Product 
{
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public decimal Subtotal { get; private set; }
    public Dimensions Dimensions { get; private set; }

    public Product(
        string description, 
        decimal price, 
        int quantity, 
        Dimensions dimensions) 
    {
        if (quantity < 0) 
        {
            throw new ArgumentException("value cannot be negative", "quantity");
        }

        Description = description;
        Price = price;
        Quantity = quantity;
        Subtotal = Price * Quantity;
        Dimensions = dimensions;
    }

    public override bool Equals(object? obj)
    {
        return obj is Product product &&
               Description == product.Description &&
               Price == product.Price &&
               Quantity == product.Quantity &&
               Subtotal == product.Subtotal &&
               Dimensions == product.Dimensions;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Description, Price, Quantity, Subtotal, Dimensions);
    }
}