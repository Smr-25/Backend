

using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplicationConsume.Filters;

public class AddAuthTokenFilter : IAsyncActionFilter
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AddAuthTokenFilter(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Read the authentication token from the cookie
        var token = context.HttpContext.Request.Cookies["AuthToken"];

        // Store the token in HttpContext.Items so it can be accessed by controllers
        if (!string.IsNullOrEmpty(token))
        {
            context.HttpContext.Items["AuthToken"] = token;
        }

        // Continue with the action execution
        await next();
    }
}
