namespace Cccat10RefactoringDomain.DTOs;

public record ProductIdQuantityDTO
{
    public long Id { get; init; }
    public int Quantity { get; init; }
}
