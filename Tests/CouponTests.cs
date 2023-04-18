using Cccat10RefactoringDomain.Models;

namespace Cccat10RefactoringTests;

public class CouponTests
{
    [TestCase(-0.1)]
    [TestCase(101)]
    public void CreateCoupon_InvalidDiscount_ThrowsException(decimal discount) 
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Coupon("VALE20", discount, DateTime.Now));
    }

    [TestCase(100, 100, 0)]
    [TestCase(50, 500, 250)]
    [TestCase(27.6, 624.21, 451.92804)]
    public void Coupon_ApplyDiscountToValue_ReturnsResult(decimal discount, decimal value, decimal expected)
    {
        var coupon = new Coupon("VALE20", discount, DateTime.Now.AddHours(1));
        var actual = coupon.ApplyDiscountTo(value);
        Assert.That(actual, Is.EqualTo(expected));
    }
}