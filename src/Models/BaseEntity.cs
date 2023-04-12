abstract public class BaseEntity 
{
    public Guid Guid { get; } = Guid.NewGuid();
}