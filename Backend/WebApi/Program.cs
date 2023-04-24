using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Presentation;
using Serilog;
using WebApi.Middlewares;
using WebApi.RoutesTransformers;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services
    .AddControllers()
    .AddApplicationPart(Presentation.AssemblyReference.Assembly);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(
        new RouteTokenTransformerConvention(new SpinalCaseParameterTransformer()));
});

// Add DI 
builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddPresentation();

builder.Services.AddSwaggerGen();

// Configure structured logging
builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); });

// Add DB context
builder.Services.AddDbContext<DbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AirleganceDB"));
});

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
    .WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

app.Run();