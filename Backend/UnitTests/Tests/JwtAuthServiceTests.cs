using Application.AutoMapper;
using Application.Dto;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using UnitTests.Mocks;

namespace UnitTests.Tests;

public class JwtAuthServiceTests
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IValidator<UserLoginRequestDto> _loginValidator;

    public JwtAuthServiceTests()
    {
        // Common
        _configuration = new MockConfiguration();

        var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>());
        _mapper = config.CreateMapper();

        _loginValidator = new UserLoginDtoValidator();
    }

    [Fact]
    public async Task GetUserWithRolesByEmailAsync_UserExists()
    {
        // Arange 
        var email = "mariaioana@example.com";
        var role = "Admin";
        var usersList = new List<User>();
        usersList.Add(new User
        {
            FirstName = "Maria",
            LastName = "Ioana",
            Email = email,
            Password = "P@ssw0rd",
            Roles = new List<Role>
            {
                new()
                {
                    Name = role
                }
            }
        });

        var repo = new MockUsersRepository(usersList);
        var unitOfWork = new MockUnitOfWork(repo);
        var jwtAuthService = new JwtAuthService(_configuration, unitOfWork, _mapper, _loginValidator);

        var response = await jwtAuthService.GetUserWithRolesAndTokenByEmailAsync(email);

        Assert.NotNull(response);
        Assert.Equal(email, response.Email);
        Assert.NotNull(response.Roles.Where(r => r.Name == role));
    }

    [Fact]
    public async Task GetUserWithRolesByEmailAsync_NoUser()
    {
        var repo = new MockUsersRepository();
        var unitOfWork = new MockUnitOfWork(repo);
        var jwtAuthService = new JwtAuthService(_configuration, unitOfWork, _mapper, _loginValidator);

        var email = "asdasd";
        //var role = "asdasd";

        var response = await jwtAuthService.GetUserWithRolesAndTokenByEmailAsync(email);

        Assert.Null(response);
    }


}