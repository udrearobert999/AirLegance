using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Core;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtAuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly string _key;

    public JwtAuthService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

        _key = configuration["Jwt:Key"] ?? string.Empty;
    }

    public string CreateJwt(AuthUserDto user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
        };

        var token = new JwtSecurityToken(
            issuer: user.Id.ToString(),
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<AuthUserDto?> AuthenticateUser(UserLoginDto userDto)
    {
        var user = await _unitOfWork.Users.GetFirstAsync(u => u.Email == userDto.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
            return null;


        var authUser = _mapper.Map<AuthUserDto>(user);

        return authUser;
    }
}