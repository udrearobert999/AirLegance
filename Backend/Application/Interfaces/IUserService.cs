using Application.Dto;

namespace Application.Interfaces;

public interface IUserService
{
    public Task<ResponseDto<UserRegistrationResponseDto?>> CreateUserAsync(
        UserRegistrationRequestDto userRegistrationRequestDto);
}