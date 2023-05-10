using Application.AutoMapper;
using Application.Dto;
using Application.Validators;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using UnitTests.Mocks;

namespace UnitTests.Tests
{
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
    }
}
