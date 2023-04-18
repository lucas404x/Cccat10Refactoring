namespace Cccat10RefactoringDomain.Entities;

public abstract class BaseEntity
{
    public Guid Guid { get; } = Guid.NewGuid();
}