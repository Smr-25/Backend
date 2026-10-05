namespace TagHelpersExample.Models;

public class Madel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MarkaId { get; set; }
    public Marka Marka { get; set; }
}