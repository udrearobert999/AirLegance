using Application.Dto;

namespace Application.Interfaces;

public interface IAuthService
{
    public Task<ResponseDto<JwtAuthResponse?>> AuthenticateUserAsync(UserLoginRequestDto userRequestDto);
}