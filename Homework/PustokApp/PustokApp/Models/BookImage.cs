using PustokApp.Models.Common;

namespace PustokApp.Models;

public class BookImage : BaseEntity
{
    public string ImageUrl { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; }
}
