namespace Domain.Entities;

public class UserToken : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; } = null!;

    public User User { get; set; } = null!;
}