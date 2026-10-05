using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnionArchApp.Application.Profiles;
using OnionArchApp.Application.Services.Concretes;
using OnionArchApp.Application.Services.Interfaces;

namespace OnionArchApp.Application;

public static class ServiceRegistration
{
    extension(IServiceCollection services){
        public void AddApplicationServices()
        {
            services.AddAutoMapper(opt=> opt.AddProfile<MapProfile>());
            services.AddValidatorsFromAssembly(typeof(ServiceRegistration).Assembly);
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IColorService, ColorService>();
        }
    }
}