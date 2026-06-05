using Microsoft.AspNetCore.Mvc;
using OnionArchApp.Application.Dtos.Product;
using OnionArchApp.Application.Services.Interfaces;

namespace OnionArchApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products =  await productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto)
    {
        var response = await productService.CreateProductAsync(productCreateDto);
        return Ok(response);
    }
    
    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductUpdateDto productUpdateDto)
    {
        await productService.UpdateProductAsync(productId, productUpdateDto);
        return NoContent();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        await productService.DeleteProductAsync(productId);
        return NoContent();
    }
}

