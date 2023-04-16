using Cccat10RefactoringDomain.ValueObjects;

namespace Cccat10RefactoringDomain.DTOs;

public class CheckoutDTO
{
    public CPF CPF { get; init; } = null!;
    public List<ProductCheckoutDTO> Products { get; init; } = null!;
    public Guid? CouponId { get; init; }
    public string? From { get; init; }
    public string? To { get; init; }
}
