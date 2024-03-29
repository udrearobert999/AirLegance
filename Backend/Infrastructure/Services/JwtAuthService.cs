﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Core;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace Infrastructure.Services;

public class JwtAuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UserLoginRequestDto> _loginValidator;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly string _key;

    public JwtAuthService(IUnitOfWork unitOfWork, IMapper mapper,
        IValidator<UserLoginRequestDto> loginValidator, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loginValidator = loginValidator;
        _config = config;

        _key = _config["Jwt:Secret"] ?? throw new InvalidOperationException("Jwt secret not found!");
    }

    public struct AuthTokensDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public async Task<AuthResponseDto> AuthenticateUserAsync(UserLoginRequestDto userRequestDto)
    {
        var validationResult = await _loginValidator.ValidateAsync(userRequestDto);

        if (!validationResult.IsValid)
        {
            return new AuthResponseDto
            {
                Response = ResponseDto<UserAuthResponseDto?>.Failure(validationResult.Errors)
            };
        }

        var user = await GetUserWithRolesAndTokenByEmailAsync(userRequestDto.Email);

        if (user is null)
        {
            var userNotFound = new ValidationFailure(nameof(userRequestDto.Email), "User not found!");

            return new AuthResponseDto
            {
                Response = ResponseDto<UserAuthResponseDto?>.Failure(userNotFound)
            };
        }

        if (!BCrypt.Net.BCrypt.Verify(userRequestDto.Password, user.Password))
        {
            var incorrectPassword = new ValidationFailure(nameof(userRequestDto.Password), "Incorrect password!");

            return new AuthResponseDto
            {
                Response = ResponseDto<UserAuthResponseDto?>.Failure(incorrectPassword)
            };
        }

        return await AuthenticateUserInternalAsync(user);
    }

    private ClaimsPrincipal ValidateToken(string token)
    {
        var tokenValidationParameters = TokenValidationParametersConfiguration.GetTokenValidationParameters(_key);

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
        return principal;
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
    {
        try
        {
            var claimsPrincipal = ValidateToken(refreshToken);
        }
        catch
        {
            var invalidTokenMessage = new ValidationFailure("RefreshToken", "Token is invalid!");

            return new AuthResponseDto
            {
                Response = ResponseDto<UserAuthResponseDto?>.Failure(invalidTokenMessage)
            };
        }

        var user = await GetUserWithRolesByRefreshToken(refreshToken);

        if (user is null)
        {
            var userNotFound = new ValidationFailure("User", "User does not have a token!");

            return new AuthResponseDto
            {
                Response = ResponseDto<UserAuthResponseDto?>.Failure(userNotFound)
            };
        }

        return await AuthenticateUserInternalAsync(user);
    }

    public async Task<bool> InvalidateTokenAsync(string refreshToken)
    {
        var user = await _unitOfWork.Users.GetUserWithTokenByRefreshToken(refreshToken);

        if (user == null)
        {
            return false;
        }

        user.UserToken = null;
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private async Task<AuthResponseDto> AuthenticateUserInternalAsync(User user)
    {
        var authTokens = CreateAuthTokens(user);

        if (user.UserToken == null)
        {
            user.UserToken = new UserToken
            {
                UserId = user.Id,
                Token = authTokens.RefreshToken
            };
        }
        else
        {
            user.UserToken.Token = authTokens.RefreshToken;
        }

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        var authUserResponse = _mapper.Map<UserAuthResponseDto>(user);

        var jwtDto = new AuthResponseDto
        {
            AccessToken = authTokens.AccessToken,
            RefreshToken = authTokens.RefreshToken,
            Response = ResponseDto<UserAuthResponseDto?>.Success(authUserResponse)
        };

        return jwtDto;
    }

    public AuthTokensDto CreateAuthTokens(User user)
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

        var tokenHandler = new JwtSecurityTokenHandler();
        var accessToken = new JwtSecurityToken(
            issuer: user.Id.ToString(),
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        var refreshToken = new JwtSecurityToken(
            issuer: user.Id.ToString(),
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddDays(2),
            signingCredentials: credentials
        );

        var tokens = new AuthTokensDto
        {
            AccessToken = tokenHandler.WriteToken(accessToken),
            RefreshToken = tokenHandler.WriteToken(refreshToken)
        };

        return tokens;
    }

    public async Task<User?> GetUserWithRolesAndTokenByEmailAsync(string email)
    {
        var user = await _unitOfWork.Users.GetUserWithRolesAndTokenByEmailAsync(email);

        return user;
    }

    public async Task<User?> GetUserWithRolesByRefreshToken(string refreshToken)
    {
        var user = await _unitOfWork.Users.GetUserWithRolesAndTokenByRefreshToken(refreshToken);

        return user;
    }
}