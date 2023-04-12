namespace Cccat10RefactoringCode.Models;

public class OrderItem : BaseEntity
{
    private const short FEE_DISTANCE_KM = 1000;

    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public double Weight { get; private set; }
    public Dimensions Dimensions { get; private set; }
    public decimal Subtotal => Math.Round(Price * Quantity, 2);
    public double Density => Math.Round(Weight / Dimensions.Volume);

    public OrderItem(
        string description, 
        decimal price, 
        Dimensions dimensions, 
        int quantity,
        double weight)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("value cannot be negative", "quantity");
        }
        if (weight < 0) 
        {
            throw new ArgumentException("value cannot be negative", "weight");
        }
        Description = description;
        Price = price;
        Dimensions = dimensions;
        Quantity = quantity;
        Weight = weight;
    }

    public double GetFeeTax() 
    {
        var result = Math.Round(FEE_DISTANCE_KM * Dimensions.Volume * (Density / 100), 2);
        return result >= 10 ? result : 10;
    }
}