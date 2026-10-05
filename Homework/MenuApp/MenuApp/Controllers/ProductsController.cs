using MenuApp.Data;
using MenuApp.Dtos.Products;
using MenuApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MenuApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await dbContext.Products
            .Include(p => p.Category)
            .Select(p => new ProductReturnDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            })
            .ToListAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await dbContext.Products
            .Include(p => p.Category)
            .Where(p => p.Id == id)
            .Select(p => new ProductReturnDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                IsNew =  p.IsNew,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            })
            .FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProduct(CreateProductDto productDto)
    {
        var categoryExists = await dbContext.Categories.AnyAsync(c => c.Id == productDto.CategoryId);
        if (!categoryExists)
        {
            return BadRequest("Category not found");
        }

        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
            Stock = productDto.Stock,
            CategoryId = productDto.CategoryId
        };

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        var createdProduct = await dbContext.Products
            .Include(p => p.Category)
            .Where(p => p.Id == product.Id)
            .Select(p => new ProductReturnDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            })
            .FirstOrDefaultAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, createdProduct);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto productDto)
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        var categoryExists = await dbContext.Categories.AnyAsync(c => c.Id == productDto.CategoryId);
        if (!categoryExists)
        {
            return BadRequest("Category not found");
        }

        product.Name = productDto.Name;
        product.Price = productDto.Price;
        product.Stock = productDto.Stock;
        product.CategoryId = productDto.CategoryId;

        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }
}