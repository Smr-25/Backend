using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApplicationConsume.Models;

namespace WebApplicationConsume.Controllers;

public class CategoryController(IHttpClientFactory httpClientFactory) : Controller
{
    public async Task<IActionResult> Index()
    {
        try
        {
            // Create HttpClient with the AuthTokenHandler (token is added automatically)
            var client = httpClientFactory.CreateClient("ApiClient");

            // Send GET request to the API (token is automatically added by the handler)
            var response = await client.GetAsync("http://localhost:5180/api/Category");

            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response
                var responseContent = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<CategoryReturnDto>>(responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return View(categories ?? new List<CategoryReturnDto>());
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // If unauthorized, redirect to login
                TempData["ErrorMessage"] = "You need to login to view categories.";
                return RedirectToAction("Login", "UiAccount");
            }
            else
            {
                // Handle error - return empty list or show error message
                TempData["ErrorMessage"] = "Failed to load categories from the API.";
                return View(new List<CategoryReturnDto>());
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            return View(new List<CategoryReturnDto>());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            // Create HttpClient with the AuthTokenHandler (token is added automatically)
            var client = httpClientFactory.CreateClient("ApiClient");

            // Send GET request to the API for specific category (token is automatically added)
            var response = await client.GetAsync($"http://localhost:5180/api/Category/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response
                var responseContent = await response.Content.ReadAsStringAsync();
                var category = JsonSerializer.Deserialize<CategoryReturnDto>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (category == null)
                {
                    TempData["ErrorMessage"] = "Category not found.";
                    return RedirectToAction("Index");
                }

                return View(category);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // If unauthorized, redirect to login
                TempData["ErrorMessage"] = "You need to login to view category details.";
                return RedirectToAction("Login", "UiAccount");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                TempData["ErrorMessage"] = "Category not found.";
                return RedirectToAction("Index");
            }
            else
            {
                // Handle error
                TempData["ErrorMessage"] = "Failed to load category details from the API.";
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var client = httpClientFactory.CreateClient("ApiClient");

            // Create multipart form data content
            using var formData = new MultipartFormDataContent();

            // Add Name
            formData.Add(new StringContent(model.Name), "Name");

            // Add Description
            formData.Add(new StringContent(model.Description), "Description");

            // Add File
            if (model.File != null && model.File.Length > 0)
            {
                var fileContent = new StreamContent(model.File.OpenReadStream());
                fileContent.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue(model.File.ContentType);
                formData.Add(fileContent, "File", model.File.FileName);
            }

            // Send POST request to the API
            var response = await client.PostAsync("http://localhost:5180/api/Category", formData);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                TempData["ErrorMessage"] = "You need to login to create a category.";
                return RedirectToAction("Login", "UiAccount");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                ModelState.AddModelError("Name", "A category with this name already exists.");
                return View(model);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Failed to create category: {errorContent}");
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