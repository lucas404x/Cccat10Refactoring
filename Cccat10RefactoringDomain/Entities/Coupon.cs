using Cccat10RefactoringDomain.Entities;

namespace Cccat10RefactoringDomain.Models;

public class Coupon : BaseEntity
{
    public string? Description { get; set; }
    public decimal PercentDiscount { get; set; }
    public DateTime ExpiredDate { get; set; }

    public Coupon() { }

    public Coupon(string description, decimal percentDiscount, DateTime expiredDate) 
    {
        if (percentDiscount < 0 || percentDiscount > 100) 
            throw new ArgumentOutOfRangeException(nameof(percentDiscount), percentDiscount, "discount must be between 0% and 100%");

        Description = description;
        PercentDiscount = percentDiscount;
        ExpiredDate = expiredDate;
    }

    public decimal ApplyDiscountTo(decimal value) => value - (value * (PercentDiscount / 100));

    public bool IsValid() => DateTime.UtcNow.Date <= ExpiredDate.Date;
}