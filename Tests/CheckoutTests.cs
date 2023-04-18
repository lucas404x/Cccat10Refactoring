using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.DTOs;
using Cccat10RefactoringDomain.Repositories;
using Cccat10RefactoringDomain.Usecases;
using Cccat10RefactoringDomain.ValueObjects;
using Moq;

namespace Cccat10RefactoringTests;

public class CheckoutTests
{
    // https://www.geradorcpf.com/
    private readonly CPF _validCPF = new("68205913226");
    private readonly CPF _invalidCPF = new("68005913226");

    private Mock<IProductRepository> _productRepositoryMock = null!;
    private Mock<ICouponRepository> _couponRepositoryMock = null!;
    private Checkout _checkout = null!;

    private readonly Coupon _validCoupon = new("VALE20", 10, DateTime.UtcNow.AddDays(10));
    private readonly Coupon _invalidCoupon = new("VALE20", 10, DateTime.UtcNow);

    [SetUp]
    public void Setup()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _couponRepositoryMock = new Mock<ICouponRepository>();
        _checkout = new Checkout(_productRepositoryMock.Object, _couponRepositoryMock.Object);
    }

    [Test]
    public void Checkout_InvalidCPF_ThrowsException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => _checkout.Execute(new CheckoutDTO { CPF = _invalidCPF }));
    }

    [Test]
    public void Checkout_DuplicatedItem_ThrowsException()
    {
        var product = new Product("", 18.25M, 10, 10, 10, 10);
        _productRepositoryMock.Setup(x => x.GetProductAsync(It.IsAny<Guid>()).Result).Returns(product);

        var input = new CheckoutDTO
        {
            CPF = _validCPF,
            Products = new List<ProductIdQuantityDTO>
            {
                new ProductIdQuantityDTO
                {
                    Id = product.Guid,
                    Quantity = 5,
                },
                new ProductIdQuantityDTO
                {
                    Id = product.Guid,
                    Quantity = 2
                }
            }
        };

        Assert.ThrowsAsync<InvalidOperationException>(() => _checkout.Execute(input));
    }

    [Test]
    public async Task Checkout_TotalWithoutDiscount_ReturnsResult()
    {
        var product1 = new Product("", 18.25M, 100, 30, 10, 3);
        var product2 = new Product("", 10.60M, 20, 15, 10, 1);
        _productRepositoryMock.Setup(x => x.GetProductAsync(product1.Guid).Result).Returns(product1);
        _productRepositoryMock.Setup(x => x.GetProductAsync(product2.Guid).Result).Returns(product2);

        var input = new CheckoutDTO
        {
            CPF = _validCPF,
            Products = new List<ProductIdQuantityDTO>
            {
                new ProductIdQuantityDTO
                {
                    Id = product1.Guid,
                    Quantity = 5,
                },
                new ProductIdQuantityDTO
                {
                    Id = product2.Guid,
                    Quantity = 2
                }
            }
        };

        var actual = await _checkout.Execute(input);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Total, Is.EqualTo(112.45M));
            Assert.That(actual.FeeTax, Is.EqualTo(170));
        });
    }

    [Test]
    public async Task Checkout_TotalWithDiscount_ReturnsResult()
    {
        var product1 = new Product("", 18.25M, 100, 30, 10, 3);
        var product2 = new Product("", 10.60M, 20, 15, 10, 1);
        _productRepositoryMock.Setup(x => x.GetProductAsync(product1.Guid).Result).Returns(product1);
        _productRepositoryMock.Setup(x => x.GetProductAsync(product2.Guid).Result).Returns(product2);
        _couponRepositoryMock.Setup(x => x.GetCouponAsync(_validCoupon.Guid).Result).Returns(_validCoupon);

        var input = new CheckoutDTO
        {
            CPF = _validCPF,
            Products = new List<ProductIdQuantityDTO>
            {
                new ProductIdQuantityDTO
                {
                    Id = product1.Guid,
                    Quantity = 5,
                },
                new ProductIdQuantityDTO
                {
                    Id = product2.Guid,
                    Quantity = 2
                }
            },
            CouponId = _validCoupon.Guid,
            From = "ABC",
            To = "DEFG",
        };

        var actual = await _checkout.Execute(input);

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
        var product1 = new Product("", 18.25M, 10, 10, 10, 10);
        _productRepositoryMock.Setup(x => x.GetProductAsync(product1.Guid).Result).Returns(product1);
        _couponRepositoryMock.Setup(x => x.GetCouponAsync(_invalidCoupon.Guid).Result).Returns(_invalidCoupon);

        var input = new CheckoutDTO
        {
            CPF = _validCPF,
            Products = new List<ProductIdQuantityDTO>
            {
                new ProductIdQuantityDTO
                {
                    Id = product1.Guid,
                    Quantity = 5,
                }
            },
            CouponId = _invalidCoupon.Guid
        };

        Assert.ThrowsAsync<InvalidOperationException>(() => _checkout.Execute(input));
    }
}