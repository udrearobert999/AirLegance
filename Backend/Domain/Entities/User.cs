namespace Domain.Entities;

public class User : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Password { get; set; } = null!;

    public UserToken? UserToken { get; set; } = null!;
    public ICollection<Role> Roles { get; set; } = null!;
    public ICollection<FlightSchedule> FlightSchedules { get; set; } = null!;
}