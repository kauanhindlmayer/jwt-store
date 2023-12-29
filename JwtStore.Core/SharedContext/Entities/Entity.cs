namespace JwtStore.Core.SharedContext.Entities;

public abstract class Entity : IEquatable<Guid>
{
    public Guid Id { get; } = Guid.NewGuid();

    public bool Equals(Guid id)
    {
        return Id == id;
    }
    
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}