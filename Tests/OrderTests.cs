using Cccat10RefactoringCode.Models;

namespace Cccat10RefactoringTests;

public class OrderTests
{
    // https://www.geradorcpf.com/
    private const string VALID_CPF = "68205913226";
    private const string INVALID_CPF = "68005913226";

    private readonly Coupon _coupon = new(10, DateTime.Today.AddDays(10));
    private readonly Dimensions _dimensions = new(10, 20, 30);

    [Test]
    public void CalculatePrice_ValidArgsAndNoCoupon_ReturnsTotalPrice()
    {
        var order = new Order(VALID_CPF);
        PopulateOrderWithDefaultProducts(order);

        var actual = order.GetTotalPrice();
        Assert.That(actual, Is.EqualTo(74.05M));
    }

    [Test]
    public void CalculatePrice_ValidArgsAndWithCoupon_ReturnsTotalPriceWithDiscount()
    {
        var order = new Order(VALID_CPF, _coupon);
        PopulateOrderWithDefaultProducts(order);

        var actual = order.GetTotalPrice();
        Assert.That(actual, Is.EqualTo(66.645M));
    }

    [Test]
    public void CalculatePrice_ValidArgsAndExpiredCoupon_ThrowsException()
    {
        var order = new Order(VALID_CPF, new Coupon(10, DateTime.MinValue));
        PopulateOrderWithDefaultProducts(order);

        Assert.Throws<InvalidOperationException>(() => order.GetTotalPrice());
    }

    [Test]
    public void MakeOrder_InvalidCPF_ThrowsException() 
    {
        var order = new Order(INVALID_CPF);
        Assert.Throws<ArgumentException>(() => order.MakeOrder());
    }

    [Test]
    public void CreateOrder_AddSameProduct_ThrowsException() 
    {
        var order = new Order(VALID_CPF, _coupon);
        var product = new Product("Ovos", 5.0M, 1, _dimensions);
        Assert.Throws<ArgumentException>(() =>
        {
            order.AddProduct(product);
            order.AddProduct(product);
        });
        Assert.That(order.Products.Count(x => x.Equals(product)), Is.EqualTo(1));
    }

    private void PopulateOrderWithDefaultProducts(Order order) 
    {
        order.AddProduct(new("Soja", 18.25M, 1, _dimensions));
        order.AddProduct(new("Ovos", 10.60M, 3, _dimensions));
        order.AddProduct(new("Abacate", 6.0M, 4, _dimensions));
    }
}