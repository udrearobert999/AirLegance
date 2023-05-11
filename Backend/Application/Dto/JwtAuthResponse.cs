namespace Application.Dto;

public class JwtAuthResponse
{
    public string? Jwt { get; set; } = null;
    public ResponseDto<UserAuthResponseDto?> Response { get; set; } = null!;
}