namespace Cccat10RefactoringDomain.DTOs;

public record ProductIdQuantityDTO
{
    public Guid Id { get; init; }
    public int Quantity { get; init; }
}
