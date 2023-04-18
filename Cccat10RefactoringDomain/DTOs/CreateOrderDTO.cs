using Cccat10RefactoringDomain.ValueObjects;

namespace Cccat10RefactoringDomain.DTOs;
public class CreateOrderDTO
{
    public List<ProductIdQuantityDTO> Products { get; init; } = null!;
    public Guid Coupon { get; init; }
    public CPF CPF { get; init; } = null!;
    public string? From { get; init; }
    public string? To { get; init; }
}
