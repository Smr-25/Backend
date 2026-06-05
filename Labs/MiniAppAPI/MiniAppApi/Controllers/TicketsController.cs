using Microsoft.AspNetCore.Mvc;
using MiniAppApi.Dtos;
using MiniAppApi.Dtos.Tickets;
using MiniAppApi.Models;
using MiniAppApi.Services;

namespace MiniAppApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController(TicketService ticketService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParams paginationParams)
    {
        PaginatedResponse<TicketReturnDto> paginatedTickets = await ticketService.GetAllTicketsAsync(paginationParams);
        var response = new ApiResponse<PaginatedResponse<TicketReturnDto>>(paginatedTickets, message: "Tickets retrieved successfully");
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TicketCreateDto ticketCreateDto)
    {
        await ticketService.CreateTicketAsync(ticketCreateDto);
        var response = new ApiResponse<object?>(null, "Ticket created successfully");
        return Created(string.Empty, response);
    }
}