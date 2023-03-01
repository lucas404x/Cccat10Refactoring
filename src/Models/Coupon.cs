namespace Cccat10RefactoringCode.Models;

public class Coupon 
{
    public decimal PercentDiscount { get; init; }

    public Coupon(decimal percentDiscount) 
    {
        if (percentDiscount < 0 || percentDiscount > 100) 
            throw new ArgumentOutOfRangeException("The discount must be between 0% and 100%.");

        PercentDiscount = percentDiscount / 100;
    }

    public decimal ApplyDiscountTo(decimal value)
    {
        return value - (value * PercentDiscount);
    }
}