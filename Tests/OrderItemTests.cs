using Cccat10RefactoringDomain.DTOs;
using Cccat10RefactoringDomain.Entities;
using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.Usecases;
using Cccat10RefactoringDomain.ValueObjects;
using NUnit.Framework.Interfaces;

namespace Cccat10RefactoringTests;

public class OrderItemTests
{
    private readonly CPF _validCPF = new("68205913226");

    [TestCase(100, 30, 10, 3, 30)]
    [TestCase(200, 100, 50, 40, 400)]
    public void CalculateFeeTax_Execute_ReturnsResult(
        double width,
        double height,
        double length,
        double weight,
        double expected)
    {
        var item = new OrderItem
        {
            Product = new Product("Untitled", 1, width, height, length, weight),
            Quantity = 1,
        };
        var actual = item.GetFeeTax();
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Product_GetFeeTaxWithLowTax_ReturnsMinTax()
    {
        var item = new OrderItem
        {
            Product = new Product("Untitled", 1, 20, 15, 10, 1),
            Quantity = 1,
        };
        // The actual tax should be 9.90, but it must returns 10 because it's the min tax allowed.
        var actual = item.GetFeeTax();
        Assert.That(actual, Is.EqualTo(10));
    }

    [Test]
    public void CalculateFeeTax_ExecuteWithMultipleQuantities_ReturnsResult()
    {
        var item = new OrderItem
        {
            Product = new Product("Untitled", 1, 20, 15, 10, 1),
            Quantity = 2,
        };
        var actual = item.GetFeeTax();
        Assert.That(actual, Is.EqualTo(20));
    }
}