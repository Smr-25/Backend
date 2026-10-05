using OnionArchApp.Application.Dtos.Color;
using OnionArchApp.Application.Models;

namespace OnionArchApp.Application.Services.Interfaces;

public interface IColorService
{
    
    Task<ResponseModel<List<ColorReturnDto>>> GetAllColorsAsync();
    Task<ResponseModel<ColorReturnDto>> CreateColorAsync(ColorCreateDto color);
    Task UpdateColorAsync(int colorId, ColorUpdateDto color);
    Task DeleteColorAsync(int colorId);
}