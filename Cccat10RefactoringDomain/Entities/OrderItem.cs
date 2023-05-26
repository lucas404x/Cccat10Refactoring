using Cccat10RefactoringDomain.Models;

namespace Cccat10RefactoringDomain.Entities;

public class OrderItem : BaseEntity
{
    private const byte MIN_FEE_TAX = 10;
    private const short FEE_DISTANCE_KM = 1000;

    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }

    public double GetFeeTax()
    {
        var calculatedFeeTax = Math.Round(FEE_DISTANCE_KM * Product.Volume * (Product.Density / 100), 2);
        return Math.Max(calculatedFeeTax, MIN_FEE_TAX) * Quantity;
    }
}
