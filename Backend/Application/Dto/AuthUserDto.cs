namespace Application.Dto;

public class AuthUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}