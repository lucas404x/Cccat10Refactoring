using Cccat10RefactoringDomain.Models;

namespace Cccat10RefactoringTests;

public class ProductTests
{
    [TestCase(-1, 0, 0, 0, 0)]
    [TestCase(0, -1, 0, 0, 0)]
    [TestCase(0, 0, -1, 0, 0)]
    [TestCase(0, 0, 0, -1, 0)]
    [TestCase(0, 0, 0, 0, -1)]
    public void CreateProduct_NegativeValues_ThrowsException(
        decimal price, 
        double width, 
        double height, 
        double length, 
        double weight)
    {

        Assert.Throws<ArgumentException>(() => new Product("", price, width, height, length, weight));
    }

    [TestCase(20, 15, 10, 1, 333)]
    [TestCase(100, 30, 10, 3, 100)]
    [TestCase(200, 100, 50, 40, 40)]
    public void Product_CalculateDensity_ReturnsResult(
        double width, 
        double height, 
        double length, 
        double weight,
        double expected) 
    {
        var product = new Product("Untitled", 1, width, height, length, weight);
        var actual = product.Density;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Product_GetCalculatedVolume_ReturnsResult()
    {
        var product = new Product("Untitled", 1, 20, 15, 10, 1);
        var actual = product.Volume;
        Assert.That(actual, Is.EqualTo(0.003));
    }
}