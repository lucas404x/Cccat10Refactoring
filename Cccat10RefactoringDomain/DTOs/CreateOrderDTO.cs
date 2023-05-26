using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.ValueObjects;

namespace Cccat10RefactoringDomain.DTOs;

public record CreateOrderDTO(
    List<ProductIdQuantityDTO> Items,
    Coupon? Coupon,
    CPF CPF,
    string? From,
    string? To);
