using System.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchApp.Application.Interfaces;
using OnionArchApp.Persistance.Data;

namespace OnionArchApp.Persistance;

public static class ServiceRegistration
{
    extension(IServiceCollection services)
    {
        public void AddPersistanceServices(IConfiguration configuration)
        {
          services.AddDbContext<AppDbContext>(options =>
          {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
          });
            services.AddScoped<IApplicationDbContext, AppDbContext>();
        }
    }
}