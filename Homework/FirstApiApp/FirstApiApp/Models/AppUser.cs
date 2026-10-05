using Microsoft.AspNetCore.Identity;

namespace FirstApiApp.Models;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
}