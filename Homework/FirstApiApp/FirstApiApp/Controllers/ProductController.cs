using AutoMapper;
using FirstApiApp.Data;
using FirstApiApp.Dtos.Products;
using FirstApiApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApiApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(AppDbContext dbContext,IMapper mapper) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var products = dbContext.Products.Include(p=>p.Category).Include(p=>p.ProductColors).ThenInclude(pc=>pc.Color).ToList();
        var productsReturnDto = mapper.Map<List<ProductReturnDto>>(products);
        return Ok(productsReturnDto);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var product = dbContext.Products.Include(p=>p.Category).Include(p=>p.ProductColors).ThenInclude(pc=>pc.Color).FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();
        var productReturnDto = mapper.Map<ProductReturnDto>(product);
        return Ok(productReturnDto);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ProductCreateDto productCreateDto)
    {
        if (!dbContext.Categories.Any(c => c.Id == productCreateDto.CategoryId))
            return BadRequest();
        foreach (var colorId in productCreateDto.ColorIds)
        {
            if (!dbContext.Categories.Any(c => c.Id == colorId))
                return BadRequest();
        }
        var product = mapper.Map<Product>(productCreateDto);
        dbContext.Products.Add(product);
        dbContext.SaveChanges();
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ProductUpdateDto updatedProduct)
    {
        var existingProduct = dbContext.Products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
            return NotFound();
        if (dbContext.Categories.Any(c => c.Id == existingProduct.CategoryId))
            return BadRequest();
        mapper.Map(updatedProduct, existingProduct);
        dbContext.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = dbContext.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();
        dbContext.Products.Remove(product);
        dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPost("bulk")]
    public IActionResult PostBulky([FromBody] List<ProductCreateDto> productCreateDtos)
    {
        var products = mapper.Map<List<Product>>(productCreateDtos);
        dbContext.Products.AddRange(products);
        dbContext.SaveChanges();
        return Ok();
    }
}