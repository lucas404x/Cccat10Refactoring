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

        var actual = order.GetSubtotalPrice();
        Assert.That(actual, Is.EqualTo(74.05M));
    }

    [Test]
    public void CalculatePrice_ValidArgsAndWithCoupon_ReturnsTotalPriceWithDiscount()
    {
        var order = new Order(VALID_CPF, _coupon);
        PopulateOrder(order);

        var actual = order.GetSubtotalPrice();
        Assert.That(actual, Is.EqualTo(66.645M));
    }

    [Test]
    public void CalculatePrice_ValidArgsAndExpiredCoupon_ThrowsException()
    {
        var order = new Order(VALID_CPF, new Coupon(10, DateTime.MinValue));
        PopulateOrder(order);

        Assert.Throws<InvalidOperationException>(() => order.GetSubtotalPrice());
    }

    [Test]
    public void GetFeeTax_CalculateFeeOfAllItems_ReturnsTotalFee()
    {
        var order = new Order(VALID_CPF);
        order.AddOrderItem(new OrderItem("", 10, Dimensions.FromCentimers(20, 15, 10), 1, 1));
        order.AddOrderItem(new OrderItem("", 10, Dimensions.FromCentimers(100, 30, 10), 1, 3));
        order.AddOrderItem(new OrderItem("", 10, Dimensions.FromCentimers(200, 100, 50), 1, 40));

        var actual = order.GetFeeTax();

        Assert.That(actual, Is.EqualTo(440));
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
        var item = new OrderItem("Untitled", 20M, Dimensions.FromCentimers(20, 13, 15), 1, 1);
        Assert.Throws<ArgumentException>(() =>
        {
            order.AddOrderItem(item);
            order.AddOrderItem(item);
        });
        Assert.That(order.Items.Count(x => x.Guid.Equals(item.Guid)), Is.EqualTo(1));
    }

    private void PopulateOrder(Order order) 
    {
        var dim = Dimensions.FromCentimers(20, 13, 15);
        order.AddOrderItem(new OrderItem("Soja", 18.25M, dim, 1, 1));
        order.AddOrderItem(new OrderItem("Ovos", 10.60M, dim, 3, 1));
        order.AddOrderItem(new OrderItem("Abacate", 6.0M, dim, 4, 1));
    }
}