using Cccat10RefactoringDomain.Entities;

namespace Cccat10RefactoringDomain.Models;

public class Product : BaseEntity
{
    private const byte MIN_FEE_TAX = 10;
    private const short FEE_DISTANCE_KM = 1000;
    private const int CUBIC_DIVISOR = 1_000_000;

    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public double Width { get; private set; }
    public double Height { get; private set; }
    public double Length { get; private set; }
    public double Weight { get; private set; }
    public double Volume => Width * Height * Length / CUBIC_DIVISOR;
    public double Density => Math.Round(Weight / Volume);

    public Product(
        string description,
        decimal price,
        double width, 
        double height, 
        double length,
        double weight)
    {
        string? negativeValueParam = null;

        if (price < 0) negativeValueParam = nameof(price);
        if (width < 0) negativeValueParam = nameof(width);
        if (height < 0) negativeValueParam = nameof(height);
        if (length < 0) negativeValueParam = nameof(length);
        if (weight < 0) negativeValueParam = nameof(weight);
        
        if (!string.IsNullOrWhiteSpace(negativeValueParam))
        {
            throw new ArgumentException("value cannot be negative", negativeValueParam);
        }

        Description = description;
        Price = price;
        Width = width;
        Height = height;
        Length = length;
        Weight = weight;
    }

    public double GetFeeTax()
    {
        var calculatedFeeTax = Math.Round(FEE_DISTANCE_KM * Volume * (Density / 100), 2);
        return Math.Max(calculatedFeeTax, MIN_FEE_TAX);
    }
}