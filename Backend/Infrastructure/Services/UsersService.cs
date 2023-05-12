using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Core;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace Infrastructure.Services;

public class UsersService : IUsersService
{
    private readonly IValidator<UserRegistrationRequestDto> _userRegistrationValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UsersService(IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<UserRegistrationRequestDto> userRegistrationValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRegistrationValidator = userRegistrationValidator;
    }

    public async Task<ResponseDto<UserRegistrationResponseDto?>> CreateUserAsync(
        UserRegistrationRequestDto userRegistrationRequestDto)
    {
        var validationResult = await _userRegistrationValidator.ValidateAsync(userRegistrationRequestDto);

        var validationErrors = new List<ValidationFailure>();
        if (!validationResult.IsValid)
        {
            validationErrors.AddRange(validationResult.Errors);
        }

        if (await GetUserByEmailAsync(userRegistrationRequestDto.Email) is not null)
        {
            validationErrors.Add(new ValidationFailure(
                nameof(userRegistrationRequestDto.Email),
                "The email address is already in use"
            ));
        }

        if (validationErrors.Count > 0)
        {
            return ResponseDto<UserRegistrationResponseDto>.Failure(validationErrors);
        }

        userRegistrationRequestDto.Password = BCrypt.Net.BCrypt.HashPassword(userRegistrationRequestDto.Password);

        var user = _mapper.Map<User>(userRegistrationRequestDto);

        _unitOfWork.Users.Add(user);
        await _unitOfWork.SaveChangesAsync();

        var userIdDto = _mapper.Map<UserRegistrationResponseDto>(user);

        return ResponseDto<UserRegistrationResponseDto?>.Success(userIdDto);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _unitOfWork.Users.GetFirstAsync(u => u.Email == email, track: false);

        return user;
    }
}