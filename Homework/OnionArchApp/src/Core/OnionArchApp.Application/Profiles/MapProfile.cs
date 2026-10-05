using AutoMapper;
using AutoMapper.Internal;
using OnionArchApp.Application.Dtos.Category;
using OnionArchApp.Application.Dtos.Color;
using OnionArchApp.Application.Dtos.Product;
using OnionArchApp.Domain.Entities;

namespace OnionArchApp.Application.Profiles;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Category, CategoryReturnDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Product, ProductReturnDto>();
        CreateMap<ProductColor,ColorsInProductDto>();
        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src =>
                src.ColorIds.Select(colorId => new ProductColor { ColorId = colorId }).ToList()));
        CreateMap<ProductUpdateDto, Product>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember, destMember) =>
                    srcMember != null));
        CreateMap<Color, ColorReturnDto>();
        CreateMap<ColorCreateDto, Color>();
        CreateMap<ColorUpdateDto, Color>()
            .ForAllMembers(opt =>opt.Condition((src, dest, srcMember, destMember) => srcMember != null));
    }
}