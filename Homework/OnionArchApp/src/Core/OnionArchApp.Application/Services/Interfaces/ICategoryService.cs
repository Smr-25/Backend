using OnionArchApp.Application.Dtos.Category;
using OnionArchApp.Application.Models;
using OnionArchApp.Domain.Entities;

namespace OnionArchApp.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<ResponseModel<List<CategoryReturnDto>>> GetAllCategoriesAsync();
    Task<ResponseModel<CategoryReturnDto>> CreateCategoryAsync(CategoryCreateDto categoryDto);
    Task UpdateCategoryAsync(int categoryId, CategoryUpdateDto categoryDto);
    Task DeleteCategoryAsync(int categoryId);
}