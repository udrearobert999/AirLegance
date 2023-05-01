﻿using Application.Interfaces;
using Domain.Core;
using Domain.Entities;
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

        services.AddScoped<IReadOnlyRepository<User, Guid>, ReadOnlyRepository<User, Guid>>();
        services.AddScoped<IRepository<User, Guid>, Repository<User, Guid>>();


        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IUserService, UserService>();

        return services;
    }
}