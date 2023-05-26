using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.Repositories;
using Cccat10RefactoringDomain.Usecases;
using Cccat10RefactoringDomain.ValueObjects;
using Moq;
using Cccat10RefactoringDomain.Entities;

namespace Cccat10RefactoringTests;

public class CheckoutTests
{
    // https://www.geradorcpf.com/
    private readonly CPF _validCPF = new("68205913226");
    private readonly CPF _invalidCPF = new("68005913226");

    private Mock<IOrderRepository> _orderRepository = null!;
    private Checkout _checkout = null!;

    private readonly Coupon _validCoupon = new("VALE20", 10, DateTime.UtcNow.AddDays(10));
    private readonly Coupon _invalidCoupon = new("VALE20", 10, DateTime.UtcNow);

    [SetUp]
    public void Setup()
    {
        _orderRepository = new Mock<IOrderRepository>();
        _checkout = new Checkout(_orderRepository.Object);
    }

    [Test]
    public void Checkout_InvalidCPF_ThrowsException()
    {
        var item1 = new OrderItem
        {
            Product = new Product("", 18.25M, 100, 30, 10, 3),
            Quantity = 5,
        };
        var order = new Order(_invalidCPF, null, null, null, new List<OrderItem> { item1 });
        _orderRepository.Setup(x => x.GetOrderAsync(order.Id).Result).Returns(order);
        Assert.ThrowsAsync<ArgumentException>(() => _checkout.Execute(order.Id));
    }

    [Test]
    public async Task Checkout_TotalWithoutDiscount_ReturnsResult()
    {
        var item1 = new OrderItem 
        {
            Product = new Product("", 18.25M, 100, 30, 10, 3),
            Quantity = 5,
        };
        var item2 = new OrderItem
        {
            Product = new Product("", 10.60M, 20, 15, 10, 1),
            Quantity = 2,
        };
        var order = new Order(_validCPF, null, null, null, new List<OrderItem>() { item1, item2 });
        _orderRepository.Setup(x => x.GetOrderAsync(order.Id).Result).Returns(order);

        var actual = await _checkout.Execute(order.Id);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Total, Is.EqualTo(112.45M));
            Assert.That(actual.FeeTax, Is.EqualTo(170));
        });
    }

    [Test]
    public async Task Checkout_TotalWithDiscount_ReturnsResult()
    {
        var item1 = new OrderItem
        {
            Product = new Product("", 18.25M, 100, 30, 10, 3),
            Quantity = 5,
        };
        var item2 = new OrderItem
        {
            Product = new Product("", 10.60M, 20, 15, 10, 1),
            Quantity = 2,
        };
        var order = new Order(_validCPF, _validCoupon, "ABC", "DEFG", new List<OrderItem>() { item1, item2 });
        _orderRepository.Setup(x => x.GetOrderAsync(order.Id).Result).Returns(order);

        var actual = await _checkout.Execute(order.Id);

        Assert.Multiple(() =>
        {
            // TOTAL = (SUBTOTAL + FeeTax) - Discount
            Assert.That(actual.Total, Is.EqualTo(271.205M));
            Assert.That(actual.FeeTax, Is.EqualTo(170));
        });
    }

    [Test]
    public void Checkout_InvalidCoupon_ThrowsException()
    {
        var item1 = new OrderItem
        {
            Product = new Product("", 18.25M, 100, 30, 10, 3),
            Quantity = 5,
        };
        var order = new Order(_validCPF, _invalidCoupon, "ABC", "DEFG", new List<OrderItem>() { item1  });
        _orderRepository.Setup(x => x.GetOrderAsync(order.Id).Result).Returns(order);

        Assert.ThrowsAsync<InvalidOperationException>(() => _checkout.Execute(order.Id));
    }
}