using Cccat10RefactoringDomain.ValueObjects;

namespace Cccat10RefactoringDomain.DTOs;

public record CheckoutDTO(
    List<ProductIdQuantityDTO> Products,
    CPF CPF,
    long? CouponId = null,
    string? From = null,
    string? To = null);
