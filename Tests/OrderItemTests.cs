using Cccat10RefactoringCode.Models;

namespace Cccat10RefactoringTests;

public class OrderItemTests
{
    [Test]
    public void CreateOrderItem_NegativeQuantity_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new OrderItem("", 0, Dimensions.FromCentimers(20, 13, 15), -1, 1));
    }

    [Test]
    public void CreateOrderItem_NegativeWeight_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new OrderItem("", 0, Dimensions.FromCentimers(20, 13, 15), 1, -1));
    }
    
    [TestCase(20, 15, 10, 1, 333)]
    [TestCase(100, 30, 10, 3, 100)]
    [TestCase(200, 100, 50, 40, 40)]
    public void OrderItem_CalculateDensity_ReturnsResult(
        double width, 
        double height, 
        double length, 
        double weight,
        double expected) 
    {
        var item = new OrderItem("Untitled", 500, Dimensions.FromCentimers(width, height, length), 1, weight);
        var actual = item.Density;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(100, 30, 10, 3, 30)]
    [TestCase(200, 100, 50, 40, 400)]
    public void OrderItem_GetFeeTaxWithValidParams_ReturnsResult(
        double width, 
        double height, 
        double length, 
        double weight,
        double expected) 
    {
        var item = new OrderItem("Untitled", 500, Dimensions.FromCentimers(width, height, length), 1, weight);
        var actual = item.GetFeeTax();
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void OrderItem_GetFeeTaxWithLowTax_ReturnsMinTax() 
    {
        var item = new OrderItem("Untitled", 500, Dimensions.FromCentimers(20, 15, 10), 1, 1);
        // The actual tax should be 9.90, but it must returns 10 because it's the min tax allowed.
        var actual = item.GetFeeTax();
        Assert.That(actual, Is.EqualTo(10));
    }
}