namespace Domain.Entities;

public class Role : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<User> Users { get; set; } = null!;
}