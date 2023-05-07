using Application.Dto;
using Application.Interfaces;
using Application.Validators;
using Domain.Core;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AirleganceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AirleganceDB"));
        });

        services.AddScoped<DbContext, AirleganceDbContext>();

        // User registrations
        services.AddScoped<IReadOnlyRepository<User, Guid>, ReadOnlyRepository<User, Guid>>();
        services.AddScoped<IRepository<User, Guid>, Repository<User, Guid>>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IValidator<UserRegistrationRequestDto>, UserRegistrationDtoValidator>();
        services.AddScoped<IValidator<UserLoginRequestDto>, UserLoginDtoValidator>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services registrations
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAuthService, JwtAuthService>();

        return services;
    }
}