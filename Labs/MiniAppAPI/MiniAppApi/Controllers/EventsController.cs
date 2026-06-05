using Microsoft.AspNetCore.Mvc;
using MiniAppApi.Dtos;
using MiniAppApi.Dtos.Events;
using MiniAppApi.Dtos.Organizers;
using MiniAppApi.Dtos.Tickets;
using MiniAppApi.Models;
using MiniAppApi.Services;

namespace MiniAppApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(EventService eventService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParams paginationParams)
    {
        var paginatedEvents = await eventService.GetAllEventsAsync(paginationParams);
        var response = new ApiResponse<PaginatedResponse<EventReturnDto>>(paginatedEvents, message: "Events retrieved successfully");
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EventCreateDto eventCreateDto)
    {
        await eventService.CreateEventAsync(eventCreateDto);
        var response = new ApiResponse<object?>(null, "Event created successfully");
        return Created(string.Empty, response);
    }

    [HttpPost("{id}/banner")]
    public async Task<IActionResult> UploadEventBannerImage(int id, [FromForm] EventCreateBannerDto eventCreateBannerDto)
    {
        await eventService.UploadEventBannerImageAsync(id, eventCreateBannerDto);
        var response = new ApiResponse<object?>(null, "Banner uploaded successfully");
        return Ok(response);
    }

    [HttpGet("{eventId}/tickets")]
    public async Task<IActionResult> GetEventTickets(int eventId)
    {
        var tickets = await eventService.GetEventTicketsAsync(eventId);
        var response = new ApiResponse<List<TicketReturnDto>>(tickets, message: "Event tickets retrieved successfully");
        return Ok(response);
    }

    [HttpGet("{eventId}/organizer")]
    public async Task<IActionResult> GetOrganizerOfEvent(int eventId)
    {
        var organizer = await eventService.GetOrganizerOfEventAsync(eventId);
        var response = new ApiResponse<OrganizerReturnDto>(organizer, message: "Event organizer retrieved successfully");
        return Ok(response);
    }

    [HttpPost("{eventId}/tickets")]
    public async Task<IActionResult> CreateTicketForEvent(int eventId,
        [FromBody] TicketCreateByEventDto ticketCreateDto)
    {
        await eventService.CreateTicketForEventAsync(eventId, ticketCreateDto);
        var response = new ApiResponse<object?>(null, "Ticket created successfully");
        return Created(string.Empty, response);
    }
}