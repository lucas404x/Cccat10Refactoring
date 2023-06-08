using Cccat10RefactoringDomain.DTOs;
using Cccat10RefactoringDomain.Entities;
using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.Repositories;
using Cccat10RefactoringDomain.Usecases;
using Moq;

namespace Cccat10RefactoringTests;

public class CreateOrderTests : SharedState
{
    private Mock<IOrderRepository> _orderRepository = null!;
    private Mock<ICouponRepository> _couponRepository = null!;
    private Mock<IProductRepository> _productRepository = null!;

    private CreateOrder _sut = null!;

    [SetUp]
    public void Setup()
    {
        _orderRepository = new Mock<IOrderRepository>();
        _couponRepository = new Mock<ICouponRepository>();
        _productRepository = new Mock<IProductRepository>();
        _sut = new CreateOrder(_orderRepository.Object, _productRepository.Object, _couponRepository.Object);
    }

    [Test]
    public async Task CreateOrder_WithoutIssues_ReturnsOrder()
    {
        var item = new ProductIdQuantityDTO
        {
            Id = Guid.NewGuid(),
            Quantity = 1
        };
        var product = new Product { Id = item.Id };
        var items = new List<ProductIdQuantityDTO> { item };
        var orderItem = new OrderItem
        {
            OrderId = Guid.NewGuid(),
            Product = product,
            Quantity = 1
        };
        var input = new CreateOrderDTO(items, validCoupon.Id, validCPF.Value, null, null);
        var expected = new Order(orderItem.OrderId, validCPF, validCoupon, null, null, new List<OrderItem> { orderItem });
        
        _productRepository.Setup(x => x.GetProductAsync(item.Id)).ReturnsAsync(product);
        _couponRepository.Setup(x => x.GetCouponAsync(validCoupon.Id)).ReturnsAsync(validCoupon);
        _orderRepository.Setup(x => x.CreateOrderAsync(It.IsAny<Order>())).ReturnsAsync(expected);

        var result = await _sut.Execute(input);
        
        _couponRepository.Verify(x => x.GetCouponAsync(validCoupon.Id), Times.Once());
        _productRepository.Verify(x => x.GetProductAsync(item.Id), Times.Once());
        _orderRepository.Verify(x => x.CreateOrderAsync(It.IsAny<Order>()), Times.Once());

        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(expected.Id));
            Assert.That(result.Items.First().Id, Is.EqualTo(expected.Items.First().Id));
            Assert.That(result.Coupon!.Id, Is.EqualTo(expected.Coupon!.Id));
            Assert.That(result.From, Is.EqualTo(expected.From));
            Assert.That(result.To, Is.EqualTo(expected.To));
            Assert.That(result.CPF.Value, Is.EqualTo(expected.CPF.Value));
        });
    }

    [Test]
    public void CreateOrder_InvalidCPF_ThrowsException()
    {
        var input = new CreateOrderDTO(new(), null, invalidCPF.Value, null, null);
        Assert.ThrowsAsync<ArgumentException>(() => _sut.Execute(input));
    }

    [Test]
    public void CreateOrder_InvalidCoupon_ThrowsException()
    {
        _couponRepository.Setup(x => x.GetCouponAsync(invalidCoupon.Id)).ReturnsAsync(invalidCoupon);
        var input = new CreateOrderDTO(new(), invalidCoupon.Id, validCPF.Value, null, null);
        Assert.ThrowsAsync<ArgumentException>(() => _sut.Execute(input));
    }


    [Test]
    public void CreateOrder_ProductNotExists_ThrowsException()
    {
        var item = new ProductIdQuantityDTO
        {
            Id = Guid.Empty,
            Quantity = 1
        };
        _productRepository.Setup(x => x.GetProductAsync(item.Id).Result).Returns<Product?>(null);
        _couponRepository.Setup(x => x.GetCouponAsync(validCoupon.Id)).ReturnsAsync(validCoupon);
        var input = new CreateOrderDTO(new List<ProductIdQuantityDTO> { item }, validCoupon.Id, validCPF.Value, null, null);
        Assert.ThrowsAsync<InvalidOperationException>(() => _sut.Execute(input));
    }
}
