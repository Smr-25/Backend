namespace TagHelpersExample.Models;

public class Car
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MadelId { get; set; }
    public Madel Madel { get; set; }
}