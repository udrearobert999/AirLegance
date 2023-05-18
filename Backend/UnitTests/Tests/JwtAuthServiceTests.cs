using Application.AutoMapper;
using Application.Dto;
using Application.Validators;
using AutoMapper;
using Domain.Core;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Moq;
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
    public async Task GetUserWithRolesAndTokenByEmailAsync_UserExists()
    {
        // Arrange
        var email = "mariaioana@example.com";
        var role = "Admin";
        var mockUser = new User
        {
            FirstName = "Maria",
            LastName = "Ioana",
            Email = email,
            Password = "P@ssw0rd",
            Roles = new List<Role>
            {
                new Role {Name = role}
            },
            UserToken = new UserToken { }
        };

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.Users.GetUserWithRolesAndTokenByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(mockUser);

        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(c => c["Jwt:Secret"]).Returns("Super secret key for jwt signature");

        var mockMapper = new Mock<IMapper>();

        var mockLoginValidator = new Mock<IValidator<UserLoginRequestDto>>();
        mockLoginValidator.Setup(v =>
                v.ValidateAsync(It.IsAny<ValidationContext<UserLoginRequestDto>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var jwtAuthService = new JwtAuthService(mockUnitOfWork.Object, mockMapper.Object, mockLoginValidator.Object,
            mockConfiguration.Object);

        // Act
        var response = await jwtAuthService.GetUserWithRolesAndTokenByEmailAsync(email);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(email, response.Email);
        Assert.NotNull(response.Roles.FirstOrDefault(r => r.Name == role));
    }

    [Fact]
    public async Task GetUserWithRolesByEmailAsync_NoUser()
    {
        // Arrange
        var email = "asdasd";

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.Users.GetUserWithRolesAndTokenByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User) null);

        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(c => c["Jwt:Secret"]).Returns("Super secret key for jwt signature");

        var mockMapper = new Mock<IMapper>(); // If necessary, setup specific mapping here.

        var mockLoginValidator = new Mock<IValidator<UserLoginRequestDto>>();
        mockLoginValidator.Setup(v =>
                v.ValidateAsync(It.IsAny<ValidationContext<UserLoginRequestDto>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var jwtAuthService = new JwtAuthService(mockUnitOfWork.Object, mockMapper.Object, mockLoginValidator.Object,
            mockConfiguration.Object);

        // Act
        var response = await jwtAuthService.GetUserWithRolesAndTokenByEmailAsync(email);

        // Assert
        Assert.Null(response);
    }
}