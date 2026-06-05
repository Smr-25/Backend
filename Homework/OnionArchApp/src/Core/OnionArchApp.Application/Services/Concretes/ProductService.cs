using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OnionArchApp.Application.Dtos.Product;
using OnionArchApp.Application.Interfaces;
using OnionArchApp.Application.Models;
using OnionArchApp.Application.Services.Interfaces;
using OnionArchApp.Domain.Entities;

namespace OnionArchApp.Application.Services.Concretes;

public class ProductService(
    IApplicationDbContext applicationDbContext,
    IMapper mapper,
    IValidator<ProductCreateDto> validator) : IProductService
{
    public async Task<ResponseModel<List<ProductReturnDto>>> GetAllProductsAsync()
    {
        var products = await applicationDbContext.Products.Include(p=>p.Category).Include(p=>p.ProductColors).ThenInclude(pc=>pc.Color).ToListAsync();
        var productReturnDtos = mapper.Map<List<ProductReturnDto>>(products);
        return ResponseModel<List<ProductReturnDto>>.Success(productReturnDtos);
    }

    public async Task<ResponseModel<ProductReturnDto>> CreateProductAsync(ProductCreateDto productDto)
    {
        if (await applicationDbContext.Products.AnyAsync(p => p.Name == productDto.Name))
            throw new ValidationException("A product with the same name already exists.");
        var validationResult = await validator.ValidateAsync(productDto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.ToString());
        var product = mapper.Map<Product>(productDto);
        await applicationDbContext.Products.AddAsync(product);
        await applicationDbContext.SaveChangesAsync();

        var productReturnDto = mapper.Map<ProductReturnDto>(product);
        return ResponseModel<ProductReturnDto>.Success(productReturnDto);
    }

    public async Task UpdateProductAsync(int productId, ProductUpdateDto product)
    {
        var existingProduct = await applicationDbContext.Products.FindAsync(productId);
        if (existingProduct == null)
            throw new KeyNotFoundException("Product not found.");
        mapper.Map(product, existingProduct);
        applicationDbContext.Products.Update(existingProduct);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int productId)
    {
        var existingProduct = await applicationDbContext.Products.FindAsync(productId);
        if (existingProduct == null)
            throw new KeyNotFoundException("Product not found.");
        applicationDbContext.Products.Remove(existingProduct);
        await applicationDbContext.SaveChangesAsync();
    }
}