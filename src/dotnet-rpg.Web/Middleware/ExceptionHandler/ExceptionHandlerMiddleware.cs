using System.Net;
using System.Text.Json;
using dotnet_rpg.Application.Dtos;
using dotnet_rpg.Application.Exceptions;

namespace dotnet_rpg.Web.Middleware.ExceptionHandler;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    
    public ExceptionHandlerMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext httpContext,
        Exception exception)
    {
        _logger.LogError(exception.Message);

        HttpStatusCode statusCode;
        HttpResponse response = httpContext.Response;

        switch (exception)
        {
            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                break;
        }
        
        response.ContentType = "application/json";
        response.StatusCode = (int)statusCode;

        ExceptionDto exceptionDto = new()
        {
            Message = exception.Message,
            StatusCode = (int)statusCode
        };

        var result = JsonSerializer.Serialize(exceptionDto);

        await response.WriteAsJsonAsync(result);
    }
}