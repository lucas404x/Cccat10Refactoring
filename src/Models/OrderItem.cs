namespace Cccat10RefactoringCode.Models;

public class OrderItem : BaseEntity
{
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public Dimensions Dimensions { get; private set; }
    public int Quantity { get; private set; }
    public decimal Subtotal => Price * Quantity;

    public OrderItem(string description, decimal price, Dimensions dimensions, int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("value cannot be negative", "quantity");
        }
        Description = description;
        Price = price;
        Dimensions = dimensions;
        Quantity = quantity;
    }

    public static OrderItem FromProduct(Product product, int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("value cannot be negative", "quantity");
        }
        return new OrderItem(product.Description, product.Price, product.Dimensions, quantity);
    }
}