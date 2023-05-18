namespace Cccat10RefactoringDomain.Entities;

public class OrderItem : BaseEntity
{
    public long ProductId { get; set; }
    public long ProductQuantity { get; set; }
}
