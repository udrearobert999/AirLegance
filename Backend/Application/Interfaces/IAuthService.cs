using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAuthService
{
    public Task<AuthResponse> AuthenticateUserAsync(UserLoginRequestDto userRequestDto);
    public Task<AuthResponse> RefreshTokenAsync(string refreshToken);
    public Task<bool> InvalidateTokenAsync(string refreshToken);
    public Task<User?> GetUserWithRolesAndTokenByEmailAsync(string email);
    public Task<User?> GetUserWithRolesByRefreshToken(string refreshToken);
}