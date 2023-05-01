using Application.Dto;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<UserRegistrationDto>, UserRegistrationDtoValidator>();

        return services;
    }
}