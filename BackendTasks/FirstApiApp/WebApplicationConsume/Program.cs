using WebApplicationConsume.Filters;
using WebApplicationConsume.Handlers;

namespace WebApplicationConsume;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews(options =>
        {
            // Register the action filter globally
            options.Filters.Add<AddAuthTokenFilter>();
        });

        // Add HttpContextAccessor to access HttpContext in the handler
        builder.Services.AddHttpContextAccessor();

        // Register the AuthTokenHandler
        builder.Services.AddTransient<AuthTokenHandler>();

        // Add HttpClient factory with the AuthTokenHandler
        builder.Services.AddHttpClient("ApiClient")
            .AddHttpMessageHandler<AuthTokenHandler>();

        // Also add default HttpClient for backward compatibility
        builder.Services.AddHttpClient();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}