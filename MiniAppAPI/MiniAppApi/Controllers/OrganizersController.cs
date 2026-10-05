using Microsoft.AspNetCore.Mvc;
using MiniAppApi.Dtos;
using MiniAppApi.Dtos.Events;
using MiniAppApi.Dtos.Organizers;
using MiniAppApi.Models;
using MiniAppApi.Services;

namespace MiniAppApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizersController(OrganizerService organizerService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParams paginationParams)
    {
        var paginatedOrganizers = await organizerService.GetAllOrganizersAsync(paginationParams);
        var response = new ApiResponse<PaginatedResponse<OrganizerReturnDto>>(paginatedOrganizers, message: "Organizers retrieved successfully");
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrganizerCreateDto organizerCreateDto)
    {
        await organizerService.CreateOrganizerAsync(organizerCreateDto);
        var response = new ApiResponse<object?>(null, "Organizer created successfully");
        return Created(string.Empty, response);
    }

    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadOrganizerLogoImage(int id, [FromForm] OrganizerCreateLogoDto organizerCreateLogoDto)
    {
        await organizerService.UploadOrganizerLogoImageAsync(id, organizerCreateLogoDto);
        var response = new ApiResponse<object?>(null, "Logo uploaded successfully");
        return Ok(response);
    }

    [HttpGet("{organizerId}/events")]
    public async Task<IActionResult> GetOrganizerEvents(int organizerId)
    {
        var events = await organizerService.GetOrganizerEventsAsync(organizerId);
        var response = new ApiResponse<List<EventReturnDto>>(events, message: "Organizer events retrieved successfully");
        return Ok(response);
    }
}