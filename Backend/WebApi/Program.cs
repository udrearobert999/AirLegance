using System.Text.Json.Serialization;
using AirLegance.Application.Helpers;
using AirLegance.Application.Interfaces;
using AirLegance.Application.Services;
using AirLegance.RESTService.ExceptionHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventLog;
using Serilog;

namespace AirLegance.RESTService
{
    public class Program
    {
        public static WebApplicationBuilder Initialize(string[]? args)
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                ContentRootPath = AppContext.BaseDirectory,
                Args = args
            });

            builder.Logging.SetMinimumLevel(LogLevel.Error);
            builder.Host.UseSerilog((_, lc) => lc.WriteTo.File(AppContext.BaseDirectory + "/Logs/log.txt",
                Serilog.Events.LogEventLevel.Error, "[{Timestamp:HH:mm:ss}{Level:u3}]{Message}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day, retainedFileCountLimit: null));

            builder.Configuration.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

            // Add services to the container.

            builder.Host.UseWindowsService();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMvc()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            builder.Services.Configure<EventLogSettings>(config =>
            {
                config.LogName = string.Empty;
                config.SourceName = "AirLegance";
            });

            builder.Services.AddDbContext<DbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
            }); // Change DB

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            builder.Services.AddTransient<ITestService, TestTestService>();

            return builder;
        }

        public static void Main(string[] args)
        {
            var builder = Initialize(args);
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}