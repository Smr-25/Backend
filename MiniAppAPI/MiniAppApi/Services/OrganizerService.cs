using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniAppApi.Data;
using MiniAppApi.Dtos;
using MiniAppApi.Dtos.Events;
using MiniAppApi.Dtos.Organizers;
using MiniAppApi.Exceptions;
using MiniAppApi.Models;
using MiniAppApi.Utils;

namespace MiniAppApi.Services;

public class OrganizerService(AppDbContext dbContext, IMapper mapper, FileManager fileManager)
{
    public async Task<PaginatedResponse<OrganizerReturnDto>> GetAllOrganizersAsync(PaginationParams paginationParams)
    {
        var query = dbContext.Organizers.Include(o => o.Events).AsQueryable();
        
        var totalCount = await query.CountAsync();
        
        var organizers = await query
            .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();
        
        var organizerDto = mapper.Map<List<OrganizerReturnDto>>(organizers);
        
        return new PaginatedResponse<OrganizerReturnDto>
        {
            Items = organizerDto,
            TotalCount = totalCount,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
    }
    
    public async Task CreateOrganizerAsync(OrganizerCreateDto organizerCreateDto)
    {
        var organizer = mapper.Map<Organizer>(organizerCreateDto);
        await dbContext.Organizers.AddAsync(organizer);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task UploadOrganizerLogoImageAsync(int organizerId, OrganizerCreateLogoDto organizerCreateLogoDto)
    {
        var organizer = await dbContext.Organizers.FindAsync(organizerId);
        if (organizer == null)
            throw new EntityNotFoundException(nameof(Organizer), organizerId);
        
        var path = await fileManager.SaveOrganizerLogoAsync(organizerId, organizerCreateLogoDto.LogoImage);
        organizer.LogoImageUrl = path;
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<List<EventReturnDto>> GetOrganizerEventsAsync(int organizerId)
    {
        var organizer = await dbContext.Organizers
            .Include(o => o.Events).ThenInclude(e => e.Tickets)
            .FirstOrDefaultAsync(o => o.Id == organizerId);
        if (organizer == null)
            throw new EntityNotFoundException(nameof(Organizer), organizerId);
        
        var eventDto = mapper.Map<List<EventReturnDto>>(organizer.Events);
        return eventDto;
    }
}
