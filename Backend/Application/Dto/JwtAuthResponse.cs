namespace Application.Dto;

public class AuthResponse
{
    public string? AccessToken { get; set; } = null;
    public string? RefreshToken { get; set; } = null;
    public ResponseDto<UserAuthResponseDto?> Response { get; set; } = null!;
}