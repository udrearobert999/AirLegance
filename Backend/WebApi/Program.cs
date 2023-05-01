using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Presentation;
using Serilog;
using WebApi.Middlewares;
using WebApi.RoutesTransformers;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services
    .AddControllers()
    .AddApplicationPart(AssemblyReference.Assembly);

builder.Services.AddCors();

builder.Services.AddControllers(options =>
{   
    options.Conventions.Add(
        new RouteTokenTransformerConvention(new SpinalCaseParameterTransformer()));
});

// Add DI 
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

builder.Services.AddSwaggerGen();

// Configure structured logging
builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => options
    .WithOrigins("http://127.0.0.1:3000")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

app.Run();