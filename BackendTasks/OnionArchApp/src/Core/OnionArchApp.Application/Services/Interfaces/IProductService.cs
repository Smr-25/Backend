using OnionArchApp.Application.Dtos.Product;
using OnionArchApp.Application.Models;
using OnionArchApp.Domain.Entities;

namespace OnionArchApp.Application.Services.Interfaces;

public interface IProductService
{
    Task<ResponseModel<List<ProductReturnDto>>> GetAllProductsAsync();
    Task<ResponseModel<ProductReturnDto>> CreateProductAsync(ProductCreateDto product);
    Task UpdateProductAsync(int productId, ProductUpdateDto product);
    Task DeleteProductAsync(int productId);
}