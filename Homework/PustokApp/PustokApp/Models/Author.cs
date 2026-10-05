using System.ComponentModel.DataAnnotations;
using PustokApp.Models.Common;

namespace PustokApp.Models;

public class Author : BaseEntity
{
    [Required] [MaxLength(20)] 
    public string FullName { get; set; }
    public List<Book>? Books { get; set; }
}