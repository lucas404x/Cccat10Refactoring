namespace Cccat10RefactoringDomain.DTOs;

public record CheckoutResultDTO
{
    public decimal Total { get; set; }
    public double FeeTax { get; set; }
}
