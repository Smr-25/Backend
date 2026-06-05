using AutoMapper;
using MiniAppApi.Dtos.Events;
using MiniAppApi.Dtos.Organizers;
using MiniAppApi.Dtos.Tickets;
using MiniAppApi.Models;

namespace MiniAppApi.Profiles;

public class MapProfile : Profile
{
    public MapProfile(IHttpContextAccessor httpContextAccessor)
    {
        var request = httpContextAccessor.HttpContext.Request;
        var urlBuilder = new UriBuilder
        {
            Scheme = request.Scheme,
            Host = request.Host.Host,
            Port = request.Host.Port ?? (request.Scheme == "https" ? 443 : 80)
        };
        var url = urlBuilder.Uri.AbsoluteUri;
        CreateMap<Event, EventReturnDto>()
            .ForMember(dest => dest.BannerImageUrl,
                opt => opt.MapFrom(src => url +  src.BannerImageUrl));
        CreateMap<EventCreateDto, Event>();
        CreateMap<Organizer, OrganizerReturnDto>()
            .ForMember(dest => dest.LogoImageUrl,opt => opt.MapFrom(src => url +  src.LogoImageUrl));
        CreateMap<OrganizerCreateDto, Organizer>();
        CreateMap<TicketCreateDto, Ticket>();
        CreateMap<TicketCreateByEventDto, Ticket>();
        CreateMap<Ticket, TicketReturnDto>();
    }
}