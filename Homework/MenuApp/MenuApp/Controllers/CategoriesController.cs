using MenuApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MenuApp.Dtos.Categories;
using MenuApp.Dtos.Products;
using MenuApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace MenuApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(AppDbContext dbContext,ILogger<CategoriesController> logger, IMemoryCache cache,IRedisService redisService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        // var categoriesCache = (List<CategoryReturnDto>)cache.Get("categories");
        var categoriesCache = await redisService.GetValueAsync<List<CategoryReturnDto>>("categories");
        if (categoriesCache != null)
        {
            logger.LogInformation("Categories retrieved from cache.");
            return Ok(categoriesCache);
        }
        var categories = await dbContext.Categories
            .Select(c => new CategoryReturnDto
            {
                Id = c.Id,
                Name = c.Name,
                Products = dbContext.Products
                    .Where(p => p.CategoryId == c.Id)
                    .Select(p => new CategoryProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Stock = p.Stock
                    })
                    .ToList()
            })
            .ToListAsync();
        // cache.Set("categories", categories, TimeSpan.FromDays(1));
        await redisService.SetValueAsync("categories", categories, TimeSpan.FromDays(1));
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto category)
    {
        var newCategory = new Models.Category
        {
            Name = category.Name
        };
        dbContext.Categories.Add(newCategory);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await dbContext.Categories
            .Include(c => c.Products)
            .Where(c => c.Id == id)
            .Select(c => new CategoryReturnDto
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Select(p => new CategoryProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }
    [HttpPut("{id}")]
     [Authorize]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto categoryDto)
    {
        var category = await dbContext.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        category.Name = categoryDto.Name;
        await dbContext.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await dbContext.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetCategoryProducts(int id)
    {
        var category = await dbContext.Categories
            .Include(x=>x.Products).FirstOrDefaultAsync(c => c.Id == id);
        var products = category.Products.Select(x => new ProductReturnDto
        {
            Name = x.Name,
            Id = x.Id,
            Price = x.Price,
            Stock = x.Stock,
            CategoryName = x.Category.Name
        });
        return Ok(products);
    }
}