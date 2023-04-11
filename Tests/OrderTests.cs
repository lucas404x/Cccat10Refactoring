using Cccat10RefactoringCode.Models;

namespace Cccat10RefactoringTests;

public class OrderTests
{
    // https://www.geradorcpf.com/
    private const string VALID_CPF = "68205913226";
    private const string INVALID_CPF = "68005913226";

    private readonly Coupon _coupon = new(10, DateTime.Today.AddDays(10));

    [Test]
    public void CalculatePrice_ValidArgsAndNoCoupon_ReturnsTotalPrice()
    {
        var order = new Order(VALID_CPF);
        PopulateOrder(order);

        var actual = order.GetTotalPrice();
        Assert.That(actual, Is.EqualTo(74.05M));
    }

    [Test]
    public void CalculatePrice_ValidArgsAndWithCoupon_ReturnsTotalPriceWithDiscount()
    {
        var order = new Order(VALID_CPF, _coupon);
        PopulateOrder(order);

        var actual = order.GetTotalPrice();
        Assert.That(actual, Is.EqualTo(66.645M));
    }

    [Test]
    public void CalculatePrice_ValidArgsAndExpiredCoupon_ThrowsException()
    {
        var order = new Order(VALID_CPF, new Coupon(10, DateTime.MinValue));
        PopulateOrder(order);

        Assert.Throws<InvalidOperationException>(() => order.GetTotalPrice());
    }

    [Test]
    public void MakeOrder_InvalidCPF_ThrowsException() 
    {
        var order = new Order(INVALID_CPF);
        Assert.Throws<InvalidOperationException>(() => order.MakeOrder());
    }

    [Test]
    public void CreateOrder_AddSameItem_ThrowsException() 
    {
        var order = new Order(VALID_CPF, _coupon);
        var item = new OrderItem("Untitled", 20M, Dimensions.Square(5), 1);
        Assert.Throws<ArgumentException>(() =>
        {
            order.AddOrderItem(item);
            order.AddOrderItem(item);
        });
        Assert.That(order.Items.Count(x => x.Guid.Equals(item.Guid)), Is.EqualTo(1));
    }

    private void PopulateOrder(Order order) 
    {
        order.AddOrderItem(new OrderItem("Soja", 18.25M, Dimensions.Square(3), 1));
        order.AddOrderItem(new OrderItem("Ovos", 10.60M, Dimensions.Square(20), 3));
        order.AddOrderItem(new OrderItem("Abacate", 6.0M, Dimensions.Square(5), 4));
    }
}