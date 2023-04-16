using Cccat10RefactoringDomain.Models;

namespace Cccat10RefactoringDomain.DTOs;

public class CheckoutDTO
{
    public CPF CPF { get; init; } = null!;
    public List<ProductCheckoutDTO> Products { get; init; } = null!;
    public Guid? CouponId { get; init; }
}
