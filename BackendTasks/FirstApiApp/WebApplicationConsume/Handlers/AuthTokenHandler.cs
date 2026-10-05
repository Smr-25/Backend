using System.Net.Http.Headers;

namespace WebApplicationConsume.Handlers;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthTokenHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        // Get the token from HttpContext.Items (set by the action filter)
        var httpContext = _httpContextAccessor.HttpContext;
        
        if (httpContext != null && httpContext.Items.ContainsKey("AuthToken"))
        {
            var token = httpContext.Items["AuthToken"]?.ToString();
            
            if (!string.IsNullOrEmpty(token))
            {
                // Add the Authorization header with Bearer token
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // Continue with the request
        return await base.SendAsync(request, cancellationToken);
    }
}
