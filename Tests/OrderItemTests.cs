using Cccat10RefactoringCode.Models;

namespace Cccat10RefactoringTests;

public class OrderItemTests
{
    [Test]
    public void CreateOrderItem_NegativeQuantity_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new OrderItem("", 0, Dimensions.Square(5), -1));
    }
}