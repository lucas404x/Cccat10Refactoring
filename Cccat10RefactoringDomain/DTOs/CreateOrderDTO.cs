using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.ValueObjects;

namespace Cccat10RefactoringDomain.DTOs;

public record CreateOrderDTO(
    List<ProductIdQuantityDTO> Items,
    Guid? CouponId,
    string CPF,
    string? From,
    string? To);
