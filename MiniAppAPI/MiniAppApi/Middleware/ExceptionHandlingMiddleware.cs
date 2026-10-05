using MiniAppApi.Exceptions;
using MiniAppApi.Models;
using ApplicationException = MiniAppApi.Exceptions.ApplicationException;

namespace MiniAppApi.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        ApiResponse<object> response;
        
        if (exception is EntityNotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            response = new ApiResponse<object>(exception.Message, success: false);
        }
        else if (exception is ApplicationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            response = new ApiResponse<object>(exception.Message, success: false);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            response = new ApiResponse<object>("Internal server error occurred", success: false);
        }

        return context.Response.WriteAsJsonAsync(response);
    }
}

