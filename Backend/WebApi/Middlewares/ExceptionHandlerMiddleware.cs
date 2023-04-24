using WebApi.ErrorHandling;

namespace WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var messageTitle = "Something went wrong!";

            var endPointName = context.GetEndpoint()?.DisplayName;
            if (!string.IsNullOrEmpty(endPointName))
                messageTitle = $"[{endPointName}]";

            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error"
            }.ToString());

            _logger.Log(LogLevel.Error,
                $"{messageTitle} : {(ex.InnerException is not null ? ex.InnerException.Message : ex.Message)}");
        }
    }
}