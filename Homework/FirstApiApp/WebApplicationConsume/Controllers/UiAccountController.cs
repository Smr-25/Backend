using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApplicationConsume.Models;

namespace WebApplicationConsume.Controllers;

public class UiAccountController(IHttpClientFactory httpClientFactory) : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var client = httpClientFactory.CreateClient();

            // Serialize the DTO to JSON
            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send POST request to the API
            var response = await client.PostAsync("http://localhost:5180/api/Account/register", content);

            if (response.IsSuccessStatusCode)
            {
                // Registration successful, redirect to login page
                TempData["SuccessMessage"] = "Registration successful! Please login.";
                return RedirectToAction("Login");
            }
            else
            {
                // Registration failed, read error message
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, errorContent);
                return View(model);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var client = httpClientFactory.CreateClient();

            // Serialize the login model to JSON
            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send POST request to the API
            var response = await client.PostAsync("http://localhost:5180/api/Account/login", content);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<TokenResponseDto>(responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.Token))
                {
                    // Store the token in a cookie
                    Response.Cookies.Append("AuthToken", tokenResponse.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true, // Set to true in production with HTTPS
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddHours(24) // Token expires in 24 hours
                    });

                    // Login successful, redirect to home page
                    TempData["SuccessMessage"] = "Login successful!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to retrieve authentication token.");
                    return View(model);
                }
            }
            else
            {
                // Login failed, read error message
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, errorContent);
                return View(model);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            return View(model);
        }
    }
}