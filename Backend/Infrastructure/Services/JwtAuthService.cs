using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Core;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace Infrastructure.Services;

public class JwtAuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UserLoginRequestDto> _loginValidator;
    private readonly IMapper _mapper;
    private readonly string _key;

    public JwtAuthService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper,
        IValidator<UserLoginRequestDto> loginValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loginValidator = loginValidator;

        _key = configuration["Jwt:Secret"] ?? string.Empty;
    }

    public string CreateJwt(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.Email),
        };

        foreach (var role in user.Roles)
        {
            var roleClaim = new Claim(ClaimTypes.Role, role.Name);
            claims.Add(roleClaim);
        }

        var token = new JwtSecurityToken(
            issuer: user.Id.ToString(),
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<User?> GetUserWithRolesByEmailAsync(string email)
    {
        var user = await _unitOfWork.Users.GetUserWithRolesByEmailAsync(email);

        return user;
    }

    public async Task<JwtAuthResponse> AuthenticateUserAsync(UserLoginRequestDto userRequestDto)
    {
        var validationResult = await _loginValidator.ValidateAsync(userRequestDto);

        if (!validationResult.IsValid)
        {
            return new JwtAuthResponse
            {
                Response = ResponseDto<UserAuthResponseDto?>.Failure(validationResult.Errors)
            };
        }

        var user = await GetUserWithRolesByEmailAsync(userRequestDto.Email);

        if (user is null)
        {
            var userNotFound = new ValidationFailure(nameof(userRequestDto.Email), "User not found!");

            return new JwtAuthResponse
            {
                Response = ResponseDto<UserAuthResponseDto?>.Failure(userNotFound)
            };
        }

        if (!BCrypt.Net.BCrypt.Verify(userRequestDto.Password, user.Password))
        {
            var incorrectPassword = new ValidationFailure(nameof(userRequestDto.Password), "Incorrect password!");

            return new JwtAuthResponse
            {
                Response = ResponseDto<UserAuthResponseDto?>.Failure(incorrectPassword)
            };
        }

        var jwt = CreateJwt(user);
        var authUserResponse = _mapper.Map<UserAuthResponseDto>(user);

        var jwtDto = new JwtAuthResponse
        {
            Jwt = jwt,
            Response = ResponseDto<UserAuthResponseDto?>.Success(authUserResponse)
        };

        return jwtDto;
    }
}