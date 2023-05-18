using Cccat10RefactoringDomain.DTOs;
using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.Usecases;

namespace Cccat10RefactoringTests;

public class CalculateFeeTaxTests
{
    [TestCase(100, 30, 10, 3, 30)]
    [TestCase(200, 100, 50, 40, 400)]
    public void CalculateFeeTax_Execute_ReturnsResult(
        double width,
        double height,
        double length,
        double weight,
        double expected)
    {
        var product = new Product("Untitled", 1, width, height, length, weight);
        var input = new FeeTaxInputDTO
        {
            Items = new List<FeeTaxItemsInputDTO>() { new FeeTaxItemsInputDTO(product, 1) }
        };
        var calculateFeeTaxUseCase = new CalculateFeeTax();
        var actual = calculateFeeTaxUseCase.Execute(input);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Product_GetFeeTaxWithLowTax_ReturnsMinTax()
    {
        var product = new Product("Untitled", 1, 20, 15, 10, 1);
        var input = new FeeTaxInputDTO
        {
            Items = new List<FeeTaxItemsInputDTO>() { new FeeTaxItemsInputDTO(product, 1) }
        };
        var calculateFeeTaxUseCase = new CalculateFeeTax();
        // The actual tax should be 9.90, but it must returns 10 because it's the min tax allowed.
        var actual = calculateFeeTaxUseCase.Execute(input);
        Assert.That(actual, Is.EqualTo(10));
    }

    [Test]
    public void CalculateFeeTax_ExecuteWithMultipleItemsAndQuantities_ReturnsResult()
    {
        var product1 = new Product("Untitled", 1, 20, 15, 10, 1);
        var product2 = new Product("Untitled", 1, 100, 30, 10, 3);
        var input = new FeeTaxInputDTO
        {
            Items = new List<FeeTaxItemsInputDTO> 
            { 
                new FeeTaxItemsInputDTO(product1, 250),
                new FeeTaxItemsInputDTO(product2, 100)
            }
        };
        var calculateFeeTaxUseCase = new CalculateFeeTax();
        var actual = calculateFeeTaxUseCase.Execute(input);
        Assert.That(actual, Is.EqualTo(5500));
    }
}