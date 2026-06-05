using System.Text.Json;
using OnionArchApp.Application.Models;

namespace OnionArchApp.WebAPI.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var responseModel = ResponseModel<bool>.Failure(ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var serializedResponse = JsonSerializer.Serialize(responseModel);
            await context.Response.WriteAsync(serializedResponse);
        }
    }
}