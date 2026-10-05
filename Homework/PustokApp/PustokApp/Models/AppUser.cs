using Microsoft.AspNetCore.Identity;

namespace PustokApp.Models;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
}