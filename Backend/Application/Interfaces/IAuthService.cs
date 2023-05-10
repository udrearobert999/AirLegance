using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAuthService
{
    public Task<ResponseDto<JwtAuthResponse?>> AuthenticateUserAsync(UserLoginRequestDto userRequestDto);
    public Task<User?> GetUserWithRolesByEmailAsync(string email);
}