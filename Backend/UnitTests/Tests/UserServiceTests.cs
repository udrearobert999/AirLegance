using System.Linq.Expressions;
using Application.Dto;
using Application.Interfaces;
using Application.Validators;
using Infrastructure.Services;
using AutoMapper;
using Domain.Core;
using FluentValidation;
using UnitTests.Mocks;
using Application.AutoMapper;
using Domain.Entities;
using FluentValidation.Results;
using Moq;

namespace UnitTests.Tests;

public class UserServiceTests
{
    private readonly IUsersService _usersService;
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public UserServiceTests()
    {
        _usersRepository = new MockUsersRepository();
        IUnitOfWork unitOfWork = new MockUnitOfWork(_usersRepository);

        var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>());
        _mapper = config.CreateMapper();

        IValidator<UserRegistrationRequestDto> userRegistrationValidator = new UserRegistrationDtoValidator();
        _usersService = new UsersService(unitOfWork, _mapper, userRegistrationValidator);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("invalid_email")]
    [InlineData("invalid_email@")]
    public async Task CreateUserAsync_InvalidEmail(string email)
    {
        // Arrange
        var userRegistrationRequestDto = new UserRegistrationRequestDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = email,
            Password = "P@ssw0rd"
        };

        var user = new User
        {
            FirstName = userRegistrationRequestDto.FirstName,
            LastName = userRegistrationRequestDto.LastName,
            Email = userRegistrationRequestDto.Email,
            Password = userRegistrationRequestDto.Password
        };

        var mockUserRepository = new Mock<IUsersRepository>();
        mockUserRepository.Setup(repo => repo.GetFirstAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<bool>()))
            .ReturnsAsync(default(User));

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(mapper => mapper.Map<User>(It.IsAny<UserRegistrationRequestDto>())).Returns(user);

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(uow => uow.Users).Returns(mockUserRepository.Object);

        var mockValidator = new Mock<IValidator<UserRegistrationRequestDto>>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Email", "Invalid email format")
        });

        mockValidator.Setup(v => v.ValidateAsync(userRegistrationRequestDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        var usersService = new UsersService(mockUnitOfWork.Object, mockMapper.Object, mockValidator.Object);

        // Act
        var response = await usersService.CreateUserAsync(userRegistrationRequestDto);

        // Assert
        Assert.False(response.Succeeded);
        Assert.True(response != null &&
                    response.Errors.Any(e => e.PropertyName == nameof(userRegistrationRequestDto.Email)));
        Assert.Null(response.Data);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("short")]
    [InlineData("1234567891234567891234")]
    [InlineData("NOLOWER1@")]
    [InlineData("noupper1@")]
    public async Task CreateUserAsync_InvalidPassword(string password)
    {
        var userRegistrationRequestDto = new UserRegistrationRequestDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            Password = password
        };

        var response = await _usersService.CreateUserAsync(userRegistrationRequestDto);

        Assert.False(response.Succeeded);
        Assert.True(response != null &&
                    response.Errors.Any(e => e.PropertyName == nameof(userRegistrationRequestDto.Password)));
        Assert.Null(response.Data);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("J")]
    public async Task CreateUserAsync_InvalidFirstName(string firstName)
    {
        var userRegistrationRequestDto = new UserRegistrationRequestDto
        {
            FirstName = firstName,
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "P@ssw0rd"
        };

        var response = await _usersService.CreateUserAsync(userRegistrationRequestDto);

        Assert.False(response.Succeeded);
        Assert.True(response != null &&
                    response.Errors.Any(e => e.PropertyName == nameof(userRegistrationRequestDto.FirstName)));
        Assert.Null(response.Data);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("J")]
    public async Task CreateUserAsync_InvalidLastName(string lastName)
    {
        var userRegistrationRequestDto = new UserRegistrationRequestDto
        {
            FirstName = "John",
            LastName = lastName,
            Email = "john.doe@example.com",
            Password = "P@ssw0rd"
        };

        var response = await _usersService.CreateUserAsync(userRegistrationRequestDto);

        Assert.False(response.Succeeded);
        Assert.True(response != null &&
                    response.Errors.Any(e => e.PropertyName == nameof(userRegistrationRequestDto.LastName)));
        Assert.Null(response.Data);
    }

    [Fact]
    public async Task CreateUserAsync_UserAlreadyExists()
    {
        var userRegistrationRequestDto = new UserRegistrationRequestDto
        {
            FirstName = "Andrei",
            LastName = "Vaduva",
            Email = "vaduvaandrei@example.com",
            Password = "P@ssw0rd"
        };

        var user = new User
        {
            FirstName = userRegistrationRequestDto.FirstName,
            LastName = userRegistrationRequestDto.LastName,
            Email = userRegistrationRequestDto.Email,
            Password = userRegistrationRequestDto.Password
        };

        _usersRepository.Add(user);

        var response = await _usersService.CreateUserAsync(userRegistrationRequestDto);

        Assert.False(response.Succeeded);
        Assert.Null(response.Data);
    }

    [Fact]
    public async Task GetUserByEmailAsync_UserExists()
    {
        var email = "udrearobert@example.com";
        var user = new User
        {
            FirstName = "Robert",
            LastName = "Udrea",
            Email = email,
            Password = "P@ssw0rd"
        };

        _usersRepository.Add(user);

        var response = await _usersService.GetUserByEmailAsync(email);

        Assert.NotNull(response);
    }

    [Fact]
    public async Task GetUserByEmailAsync_NoUser()
    {
        var email = "mariaioana@example.com";
        var response = await _usersService.GetUserByEmailAsync(email);

        Assert.Null(response);
    }
}