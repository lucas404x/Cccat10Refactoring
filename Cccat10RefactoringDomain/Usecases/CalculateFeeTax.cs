using Cccat10RefactoringDomain.DTOs;

namespace Cccat10RefactoringDomain.Usecases;

public class CalculateFeeTax
{
    private const byte MIN_FEE_TAX = 10;
    private const short FEE_DISTANCE_KM = 1000;

    public double Execute(FeeTaxInputDTO input)
    {
        double result = 0;
        foreach (var item in input.Items)
        {
            var product = item.Product;
            var calculatedFeeTax = Math.Round(FEE_DISTANCE_KM * product.Volume * (product.Density / 100), 2);
            result += Math.Max(calculatedFeeTax, MIN_FEE_TAX) * item.Quantity;
        }
        return result;
    }
}
