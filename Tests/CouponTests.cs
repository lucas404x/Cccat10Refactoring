using Cccat10RefactoringCode.Models;

namespace Cccat10RefactoringTests;

public class CouponTests
{
    [TestCase(-0.1)]
    [TestCase(101)]
    public void CreateCoupon_InvalidDiscount_ThrowsException(decimal discount) 
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Coupon(discount, DateTime.Now));
    }
}