namespace MiniAppApi.Dtos.Events;

public class EventReturnDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; } 
    public string OrganizerName { get; set; }
    public string BannerImageUrl { get; set; }
    public int TicketsCount { get; set; } 
}




