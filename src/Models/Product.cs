namespace Cccat10RefactoringCode.Models;

public class Product : BaseEntity
{
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public Dimensions Dimensions { get; private set; }

    public Product(
        string description, 
        decimal price, 
        Dimensions dimensions) 
    {
        Description = description;
        Price = price;
        Dimensions = dimensions;
    }
}