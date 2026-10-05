using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniAppApi.Data;
using MiniAppApi.Dtos;
using MiniAppApi.Dtos.Tickets;
using MiniAppApi.Models;

namespace MiniAppApi.Services;

public class TicketService(AppDbContext dbContext, IMapper mapper)
{
    public async Task<PaginatedResponse<TicketReturnDto>> GetAllTicketsAsync(PaginationParams paginationParams)
    {
        var query = dbContext.Tickets.AsQueryable();
        
        var totalCount = await query.CountAsync();
        
        var tickets = await query
            .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();
        
        var ticketDtos = mapper.Map<List<TicketReturnDto>>(tickets);
        
        return new PaginatedResponse<TicketReturnDto>
        {
            Items = ticketDtos,
            TotalCount = totalCount,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
    }
    
    public async Task CreateTicketAsync(TicketCreateDto ticketCreateDto)
    {
        var ticket = mapper.Map<Ticket>(ticketCreateDto);
        await dbContext.Tickets.AddAsync(ticket);
        await dbContext.SaveChangesAsync();
    }
}