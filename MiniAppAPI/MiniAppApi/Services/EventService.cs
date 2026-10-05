using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniAppApi.Data;
using MiniAppApi.Dtos;
using MiniAppApi.Dtos.Events;
using MiniAppApi.Dtos.Organizers;
using MiniAppApi.Dtos.Tickets;
using MiniAppApi.Exceptions;
using MiniAppApi.Models;
using MiniAppApi.Utils;

namespace MiniAppApi.Services;

public class EventService(AppDbContext dbContext, IMapper mapper, FileManager fileManager)
{
    public async Task<PaginatedResponse<EventReturnDto>> GetAllEventsAsync(PaginationParams paginationParams)
    {
        var query = dbContext.Events.Include(e => e.Organizer).Include(e => e.Tickets).AsQueryable();
        
        var totalCount = await query.CountAsync();
        
        var events = await query
            .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();
        
        var eventsDto = mapper.Map<List<EventReturnDto>>(events);
        
        return new PaginatedResponse<EventReturnDto>
        {
            Items = eventsDto,
            TotalCount = totalCount,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
    }

    public async Task CreateEventAsync(EventCreateDto eventCreateDto)
    {
        var @event = mapper.Map<Event>(eventCreateDto);
        dbContext.Events.Add(@event);
        await dbContext.SaveChangesAsync();
    }

    public async Task UploadEventBannerImageAsync(int eventId, EventCreateBannerDto eventCreateBannerDto)
    {
        var @event = await dbContext.Events.FindAsync(eventId);
        if (@event == null)
            throw new EntityNotFoundException(nameof(Event), eventId);
        
        var path = await fileManager.SaveEventBannerAsync(eventId, eventCreateBannerDto.BannerImage);
        @event.BannerImageUrl = path;
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<List<TicketReturnDto>> GetEventTicketsAsync(int eventId)
    {
        var @event = await dbContext.Events
            .Include(e => e.Tickets)
            .FirstOrDefaultAsync(e => e.Id == eventId);
        if (@event == null)
            throw new EntityNotFoundException(nameof(Event), eventId);
        
        var ticketDto = mapper.Map<List<TicketReturnDto>>(@event.Tickets);
        return ticketDto;
    }

    public async Task<OrganizerReturnDto> GetOrganizerOfEventAsync(int id)
    {
        var @event = await dbContext.Events
            .Include(e => e.Organizer).ThenInclude(o => o.Events)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (@event == null)
            throw new EntityNotFoundException(nameof(Event), id);
        
        var organizerDto = mapper.Map<OrganizerReturnDto>(@event.Organizer);
        return organizerDto;
    }

    public async Task CreateTicketForEventAsync(int eventId, TicketCreateByEventDto ticketCreateDto)
    {
        var @event = await dbContext.Events.FindAsync(eventId);
        if (@event == null)
            throw new EntityNotFoundException(nameof(Event), eventId);
        
        var ticket = mapper.Map<Ticket>(ticketCreateDto);
        ticket.EventId = eventId;
        dbContext.Tickets.Add(ticket);
        await dbContext.SaveChangesAsync();
    }
}