namespace Cccat10RefactoringDomain.ValueObjects;

public abstract class ValueObject
{
    public virtual bool IsValid() => true;
}
