using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUsersService
{
    public Task<ResponseDto<UserRegistrationResponseDto?>> CreateUserAsync(
        UserRegistrationRequestDto userRegistrationRequestDto);

    public Task<User?> GetUserByEmailAsync(string email);
    public Task DeleteUserIfExistsByEmailAsync(string email);
}