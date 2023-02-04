using Cccat10RefactoringCode.Models;

namespace Cccat10RefactoringTests;

public class OrderTests
{
    // https://www.geradorcpf.com/
    private const string VALID_CPF = "68205913226";
    private const string INVALID_CPF = "68005913226";


    [Test]
    public void CalculatePrice_ValidArgsAndNoCoupon_ReturnsTotalPrice()
    {
        var order = new Order(VALID_CPF);
        PopulateOrderWithDefaultProducts(order);

        var actual = order.CalculatePrice();
        Assert.That(actual, Is.EqualTo(74.05M));
    }

    [Test]
    public void CalculatePrice_ValidArgsAndWithCoupon_ReturnsTotalPriceWithDiscount()
    {
        var order = new Order(VALID_CPF, new Coupon(0.1f));
        PopulateOrderWithDefaultProducts(order);

        var actual = order.CalculatePrice();
        Assert.That(actual, Is.EqualTo(7.405M));
    }

    [Test]
    public void CreateOrder_InvalidCoupon_ThrowsException() 
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Order(VALID_CPF, new Coupon(1.1f)));
    }

    [Test]
    public void CreateOrder_InvalidCPF_ThrowsException() 
    {
        Assert.Throws<ArgumentException>(() => new Order(INVALID_CPF));
    }

    private void PopulateOrderWithDefaultProducts(Order order) 
    {
        order.AddProduct(new("Soja", 18.25M, 1));
        order.AddProduct(new("Ovos", 10.60M, 3));
        order.AddProduct(new("Abacate", 6.0M, 4));
    }
}