using Application.Dto;

namespace Application.Interfaces;

public interface IUserService
{
    public Task<Guid> CreateUserAsync(UserRegistrationDto userRegistrationDto);
    public Task<bool> EmailExists(string email);
}