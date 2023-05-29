using Application.AutoMapper;
using Application.Dto;
using Application.Interfaces;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfiles));

        services.AddScoped<IValidator<UserRegistrationRequestDto>, UserRegistrationDtoValidator>();

        services.AddTransient<ILocationsService, LocationsService>();

        return services;
    }
}