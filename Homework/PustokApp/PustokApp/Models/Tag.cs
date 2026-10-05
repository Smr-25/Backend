using System.ComponentModel.DataAnnotations;
using PustokApp.Models.Common;

namespace PustokApp.Models;

public class Tag : BaseEntity
{
    [Required] public string Name { get; set; }
    public List<BookTag> BookTags { get; set; }
}