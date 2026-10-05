namespace MiniAppApi.Dtos.Organizers;

public class OrganizerReturnDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string LogoImageUrl { get; set; }
    public int EventsCount { get; set; }
}