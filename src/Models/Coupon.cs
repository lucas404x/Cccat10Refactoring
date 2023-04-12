namespace Cccat10RefactoringCode.Models;

public class Coupon : BaseEntity
{
    public decimal PercentDiscount { get; private set; }
    public DateTime ExpiredDate { get; private set; }

    public Coupon(decimal percentDiscount, DateTime expiredDate) 
    {
        if (percentDiscount < 0 || percentDiscount > 100) 
            throw new ArgumentOutOfRangeException("The discount must be between 0% and 100%.");

        PercentDiscount = percentDiscount / 100;
        ExpiredDate = expiredDate;
    }

    public decimal ApplyDiscountTo(decimal value)
    {
        return value - (value * PercentDiscount);
    }
}