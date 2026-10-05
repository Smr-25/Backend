using System.Text.Json;
using AppSettingsMultiPlatformPackage;
using FirstApiApp.Data;
using FirstApiApp.Dtos.Categories;
using FirstApiApp.Models;
using FirstApiApp.Profiles;
using FirstApiApp.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FirstApiApp;

public static class ServiceRegistration
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration,
        WebApplicationBuilder builder)
    {
        services.AddAppSettingsMultiPlatformJson(builder, "Mac");
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            }
        );
        services.AddControllers();
        services.AddValidatorsFromAssemblyContaining<CategoryCreateDtoValidator>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddAutoMapper(opt => { opt.AddProfile(new MapProfile(new HttpContextAccessor())); });
        services.AddHttpContextAccessor();
        // services.AddFluentValidationRulesToSwagger();   
        services.AddIdentity<AppUser, IdentityRole>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = true;
            opt.Password.RequireUppercase = true;
            opt.Password.RequireLowercase = true;
            opt.Password.RequiredLength = 6;
        }).AddEntityFrameworkStores<AppDbContext>();
        services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetSection("JwtSettings:Issuer").Value,
                    ValidAudience = configuration.GetSection("JwtSettings:Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:SecretKey").Value))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.Headers.Add("Token-Expired", "true");
                            string message = "The token is expired. Please renew your token.";
                            var json = JsonSerializer.Serialize(new { Message = message });
                            context.Response.ContentType = "application/json";
                            return context.Response.WriteAsync(json);
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        //services.AddAuthorization();

        services.AddScoped<JwtService>();
    }
}