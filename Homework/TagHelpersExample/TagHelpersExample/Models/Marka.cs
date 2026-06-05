namespace TagHelpersExample.Models;

public class Marka
{
    public int Id { get; set; }
    public string Name { get; set; }
    List<Madel> Madels { get; set; }
}