using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FirstApiApp.Dtos.Users;
using FirstApiApp.Models;
using FirstApiApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FirstApiApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IMapper mapper,
    IConfiguration configuration,
    JwtService jwtService)
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        var existingUser = await userManager.FindByNameAsync(userRegisterDto.UserName);
        if (existingUser != null)
        {
            return BadRequest("Username is already taken.");
        }

        var user = mapper.Map<AppUser>(userRegisterDto);
        var result = await userManager.CreateAsync(user, userRegisterDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await userManager.AddToRoleAsync(user, "User");
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var user = await userManager.FindByNameAsync(userLoginDto.UserName);
        if (user == null)
        {
            return Unauthorized("Invalid username or password.");
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, userLoginDto.Password);
        if (!isPasswordValid)
            return Unauthorized("Invalid username or password.");

        var roles = await userManager.GetRolesAsync(user);
        var tokenString = jwtService.GenerateToken(user, roles);
        return Ok(tokenString);
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var user = await userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound();

        return Ok(user);
    }
}