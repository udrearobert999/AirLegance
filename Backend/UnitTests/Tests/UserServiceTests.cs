﻿using Application.Dto;
using Application.Interfaces;
using Application.Validators;
using Infrastructure.Services;
using AutoMapper;
using Domain.Core;
using FluentValidation;
using UnitTests.Mocks;
using Application.AutoMapper;

namespace UnitTests.Tests
{
    public class UserServiceTests
    {
        private readonly IUsersService _usersService;

        public UserServiceTests()
        {
            IUsersRepository usersRepository = new MockUsersRepository();
            IUnitOfWork unitOfWork = new MockUnitOfWork(usersRepository);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>());
            var mapper = config.CreateMapper();

            IValidator<UserRegistrationRequestDto> userRegistrationValidator = new UserRegistrationDtoValidator();
            _usersService = new UsersService(unitOfWork, mapper, userRegistrationValidator);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("invalid_email")]
        [InlineData("invalid_email@")]
        public async Task CreateUserAsync_InvalidEmail(string email)
        {
            var userRegistrationRequestDto = new UserRegistrationRequestDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = email,
                Password = "P@ssw0rd"
            };

            var response = await _usersService.CreateUserAsync(userRegistrationRequestDto);

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
    }
}