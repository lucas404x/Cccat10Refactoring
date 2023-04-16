using Cccat10RefactoringDomain.Entities;

namespace Cccat10RefactoringDomain.Models;

public class Coupon : BaseEntity
{
    public string Description { get; private set; }
    public decimal PercentDiscount { get; private set; }
    public DateTime ExpiredDate { get; private set; }

    public Coupon(string description, decimal percentDiscount, DateTime expiredDate) 
    {
        if (percentDiscount < 0 || percentDiscount > 100) 
            throw new ArgumentOutOfRangeException(nameof(percentDiscount), percentDiscount, "discount must be between 0% and 100%");

        Description = description;
        PercentDiscount = percentDiscount;
        ExpiredDate = expiredDate;
    }

    public decimal ApplyDiscountTo(decimal value) => value - (value * (PercentDiscount / 100));

    public bool IsDateExpired() => ExpiredDate < DateTime.UtcNow;
}