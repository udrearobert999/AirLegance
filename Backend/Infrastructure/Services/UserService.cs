using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Core;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace Infrastructure.Services;

class UserService : IUserService
{
    private readonly IValidator<UserRegistrationDto> _userRegistrationValidator;
    private readonly IValidator<UserLoginDto> _userLoginValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, 
        IMapper mapper,
        IValidator<UserLoginDto> userLoginValidator,
        IValidator<UserRegistrationDto> userRegistrationValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userLoginValidator = userLoginValidator;
        _userRegistrationValidator = userRegistrationValidator;
    }

    public async Task<GenericResponseDto> CreateUserAsync(UserRegistrationDto userRegistrationDto)
    {
        var validationResult = await _userRegistrationValidator.ValidateAsync(userRegistrationDto);
        var response = new GenericResponseDto();
        if (!validationResult.IsValid)
        {

            response.Errors.AddRange(validationResult.Errors);

        }

        if (await GetUserByEmailAsync(userRegistrationDto.Email) is not null)
        {
            response.Errors.Add(new ValidationFailure(
                "Email",
                "The email address is already in use"
            ));

            return response;
        }

        userRegistrationDto.Password = BCrypt.Net.BCrypt.HashPassword(userRegistrationDto.Password);

        var user = _mapper.Map<User>(userRegistrationDto);
        _unitOfWork.Users.Add(user);

        await _unitOfWork.SaveChangesAsync();

        return new GenericResponseDto
        {
            Succeeded = true,
            IsValid = true,
            Data = user.Id
        };
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _unitOfWork.Users.GetFirstAsync(u => u.Email == email, track: false);

        return user;
    }
}