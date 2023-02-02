namespace Cccat10RefactoringCode.Models;

public class Coupon 
{
    public float PercentDiscount { get; init; }

    public Coupon(float percentDiscount) 
    {
        if (percentDiscount < 0f || percentDiscount > 1f) 
        {
            throw new ArgumentOutOfRangeException("The discount must be between 0 and 1.");
        }

        PercentDiscount = percentDiscount;
    }

    public decimal ApplyDiscountTo(decimal value)
    {
        return value * (decimal)PercentDiscount;
    }
}