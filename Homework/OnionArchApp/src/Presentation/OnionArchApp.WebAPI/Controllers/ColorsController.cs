using Microsoft.AspNetCore.Mvc;
using OnionArchApp.Application.Dtos.Color;
using OnionArchApp.Application.Services.Interfaces;

namespace OnionArchApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ColorsController(IColorService colorService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetColors()
    {
        var colors = await colorService.GetAllColorsAsync();
        return Ok(colors);
    }

    [HttpPost]
    public async Task<IActionResult> CreateColor([FromBody] ColorCreateDto colorCreateDto)
    {
        var createdColor = await colorService.CreateColorAsync(colorCreateDto);
        return Ok(createdColor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateColor(int id,
        [FromBody] ColorUpdateDto colorUpdateDto)
    {
        await colorService.UpdateColorAsync(id, colorUpdateDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteColor(int id)
    {
        await colorService.DeleteColorAsync(id);
        return NoContent();
    }
}