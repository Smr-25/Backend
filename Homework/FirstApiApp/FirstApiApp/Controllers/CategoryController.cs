using AutoMapper;
using FirstApiApp.Data;
using FirstApiApp.Dtos.Categories;
using FirstApiApp.Helpers;
using FirstApiApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApiApp.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CategoryController(AppDbContext dbContext, IMapper mapper, IWebHostEnvironment environment) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var categories = dbContext.Categories.Include(c=>c.Products).ToList();
        var categoriesDto = mapper.Map<List<CategoryReturnDto>>(categories);
        return Ok(categoriesDto);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var category = dbContext.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);
        if (category == null)
            return NotFound();

        var categoryDto = mapper.Map<CategoryReturnDto>(category);
        return Ok(categoryDto);
    }

    [HttpPost]
    public IActionResult Post([FromForm] CategoryCreateDto categoryCreateDto)
    {
        if (dbContext.Categories.Any(c => c.Name == categoryCreateDto.Name))
            return Conflict();
        var category = mapper.Map<Category>(categoryCreateDto);
        dbContext.Categories.Add(category);
        dbContext.SaveChanges();
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromForm] CategoryUpdateDto updatedCategory)
    {
        var category = dbContext.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
            return NotFound();
        if (dbContext.Categories.Any(c => c.Id != id && c.Name == updatedCategory.Name))
            return Conflict();
        var oldImageUrl = category.ImageUrl;
        var path = Path.Combine(environment.WebRootPath, "images", oldImageUrl);
        mapper.Map(updatedCategory, category);
        if(updatedCategory.File is not null)
            FileManager.DeleteFile(path);
        dbContext.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var category = dbContext.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
            return NotFound();
        dbContext.Categories.Remove(category);
        dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPost("bulk")]
    public IActionResult PostBulky([FromBody] List<CategoryCreateDto> categoryCreateDtos)
    {
        var categories = mapper.Map<List<Category>>(categoryCreateDtos);
        dbContext.Categories.AddRange(categories);
        dbContext.SaveChanges();
        return Ok();
    }
}