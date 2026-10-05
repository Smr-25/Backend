using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OnionArchApp.Application.Dtos.Color;
using OnionArchApp.Application.Interfaces;
using OnionArchApp.Application.Models;
using OnionArchApp.Application.Services.Interfaces;

namespace OnionArchApp.Application.Services.Concretes;

public class ColorService(IApplicationDbContext applicationDbContext,IMapper mapper, IValidator<ColorCreateDto> validator) : IColorService
{
    public async Task<ResponseModel<List<ColorReturnDto>>> GetAllColorsAsync()
    {
        var colors = await applicationDbContext.Colors.ToListAsync();
        var colorReturnDtos = mapper.Map<List<ColorReturnDto>>(colors);
        return ResponseModel<List<ColorReturnDto>>.Success(colorReturnDtos);
    }

    public async Task<ResponseModel<ColorReturnDto>> CreateColorAsync(ColorCreateDto color)
    {
        if (await applicationDbContext.Colors.AnyAsync(c=>c.Name == color.Name))
            throw new ValidationException("A color with the same name already exists.");
        var validationResult = await validator.ValidateAsync(color);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.ToString());
        var colorEntity = mapper.Map<Domain.Entities.Color>(color);
        await applicationDbContext.Colors.AddAsync(colorEntity);
        await applicationDbContext.SaveChangesAsync();
        return ResponseModel<ColorReturnDto>.Success(mapper.Map<ColorReturnDto>(colorEntity));
    }

    public async Task UpdateColorAsync(int colorId, ColorUpdateDto colorDto)
    {
        var color= await applicationDbContext.Colors.FindAsync(colorId);
        if (color == null)
            throw new KeyNotFoundException("Color not found.");
        mapper.Map(colorDto, color);
        applicationDbContext.Colors.Update(color);
        await applicationDbContext.SaveChangesAsync();
    }

    public Task DeleteColorAsync(int colorId)
    {
        var color = applicationDbContext.Colors.Find(colorId);
        if (color == null)
            throw new KeyNotFoundException("Color not found."); 
        applicationDbContext.Colors.Remove(color);
        return applicationDbContext.SaveChangesAsync();
    }
}