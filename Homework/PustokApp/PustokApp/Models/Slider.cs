using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PustokApp.Models.Common;
using PustokApp.Attributes;

namespace PustokApp.Models;

public class Slider : BaseEntity
{
    [Required] public string Title { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string ButtonText { get; set; }
    public string ButtonUrl { get; set; }
    public int Order { get; set; }

    [NotMapped]
    [FileLength(1 * 1024 * 1024)]
    [FileType("image/jpeg", "image/png", "image/gif")]
    public IFormFile? File { get; set; }
}