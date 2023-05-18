using Cccat10RefactoringDomain.Models;

namespace Cccat10RefactoringDomain.DTOs;

public record FeeTaxInputDTO
{
    public List<FeeTaxItemsInputDTO> Items { get; set; } = new();
}

public record FeeTaxItemsInputDTO(Product Product, int Quantity);