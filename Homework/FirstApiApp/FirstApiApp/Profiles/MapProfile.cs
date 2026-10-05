using AutoMapper;
using FirstApiApp.Dtos.Categories;
using FirstApiApp.Dtos.Products;
using FirstApiApp.Dtos.Users;
using FirstApiApp.Helpers;
using FirstApiApp.Models;

namespace FirstApiApp.Profiles;

public class MapProfile : Profile
{
    public MapProfile(IHttpContextAccessor httpContextAccessor)
    {
        var request = httpContextAccessor.HttpContext.Request;
        var urlBuilder = new UriBuilder
        {
            Scheme = request.Scheme,
            Host = request.Host.Host,
            Port = request.Host.Port ?? (request.Scheme == "https" ? 443 : 80)
        };
        var url = urlBuilder.Uri.AbsoluteUri;
        CreateMap<Product, ProductReturnDto>();
        CreateMap<ProductColor, ColorInProductReturnDto>();
        CreateMap<Category, CategoryInProductReturnDto>();
        CreateMap<CategoryCreateDto, Category>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.File.SaveFile("wwwroot/images/")));
        CreateMap<Category, CategoryReturnDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => url + "images/" + src.ImageUrl));
        CreateMap<Product, ProductInCategoryDto>();
        CreateMap<CategoryUpdateDto, Category>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Condition(src => src.File is not null))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.File.SaveFile("wwwroot/images/")));
        CreateMap<ProductUpdateDto, Product>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<UserRegisterDto, AppUser>();
    }
}