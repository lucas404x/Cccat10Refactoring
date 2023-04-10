using Cccat10RefactoringCode.Models;

namespace Cccat10RefactoringTests;

public class ProductTests
{
    private readonly Dimensions _dimensions = new(10, 20, 30);

    [TestCase(-1)]
    [TestCase(-99999)]
    public void CreateProduct_InvalidQuantity_ThrowsException(int quantity)
    {
        Assert.Throws<ArgumentException>(() => new Product("Ovos", 5.0M, quantity, _dimensions));
    }
}