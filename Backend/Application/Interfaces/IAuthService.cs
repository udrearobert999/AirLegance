using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAuthService
{
    public string CreateJwt(AuthUserDto user);
    public Task<AuthUserDto?> AuthenticateUser(UserLoginDto userDto);
}