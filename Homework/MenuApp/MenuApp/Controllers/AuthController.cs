using MenuApp.Data;
using MenuApp.Dtos.Auth;
using MenuApp.Models;
using MenuApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MenuApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(AppDbContext dbContext, JwtService jwtService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (await dbContext.Users.AnyAsync(u => u.Email == registerDto.Email))
        {
            return BadRequest("Email already exists");
        }

        if (await dbContext.Users.AnyAsync(u => u.Username == registerDto.Username))
        {
            return BadRequest("Username already exists");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = passwordHash
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        var token = jwtService.GenerateToken(user);

        var response = new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Email = user.Email
        };

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid email or password");
        }

        var token = jwtService.GenerateToken(user);

        var response = new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Email = user.Email
        };

        return Ok(response);
    }
}