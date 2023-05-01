using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Core;
using Domain.Entities;

namespace Infrastructure.Services;

class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> CreateUserAsync(UserRegistrationDto userRegistrationDto)
    {
        userRegistrationDto.Password = BCrypt.Net.BCrypt.HashPassword(userRegistrationDto.Password);

        var user = _mapper.Map<User>(userRegistrationDto);
        _unitOfWork.Users.Add(user);

        await _unitOfWork.SaveChangesAsync();

        return user.Id;
    }

    public async Task<bool> EmailExists(string email)
    {
        var user = await _unitOfWork.Users.GetFirstAsync(u => u.Email == email, track: false);
        
        return user is not null;
    }
}