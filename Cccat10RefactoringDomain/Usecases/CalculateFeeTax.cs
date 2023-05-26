using Cccat10RefactoringDomain.Models;

namespace Cccat10RefactoringDomain.Usecases;

public class CalculateFeeTax
{
    private const byte MIN_FEE_TAX = 10;
    private const short FEE_DISTANCE_KM = 1000;

    public static double Execute(Product product, int quantity)
    {
        var calculatedFeeTax = Math.Round(FEE_DISTANCE_KM * product.Volume * (product.Density / 100), 2);
        return Math.Max(calculatedFeeTax, MIN_FEE_TAX) * quantity;
    }
}
