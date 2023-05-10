using Application.Dto;

namespace Application.Interfaces;

public interface IUsersService
{
    public Task<ResponseDto<UserRegistrationResponseDto?>> CreateUserAsync(
        UserRegistrationRequestDto userRegistrationRequestDto);
}