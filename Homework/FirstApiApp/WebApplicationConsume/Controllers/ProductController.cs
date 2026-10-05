using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApplicationConsume.Models;

namespace WebApplicationConsume.Controllers;

public class ProductController(IHttpClientFactory httpClientFactory) : Controller
{
    public async Task<IActionResult> Index()
    {
        try
        {
            // Create HttpClient with the AuthTokenHandler (token is added automatically)
            var client = httpClientFactory.CreateClient("ApiClient");

            // Send GET request to the API
            var response = await client.GetAsync("http://localhost:5180/api/Product");

            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response
                var responseContent = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<ProductReturnDto>>(responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return View(products ?? new List<ProductReturnDto>());
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // If unauthorized, redirect to login
                TempData["ErrorMessage"] = "You need to login to view products.";
                return RedirectToAction("Login", "UiAccount");
            }
            else
            {
                // Handle error - return empty list or show error message
                TempData["ErrorMessage"] = "Failed to load products from the API.";
                return View(new List<ProductReturnDto>());
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            return View(new List<ProductReturnDto>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new ProductCreateViewModel();

        try
        {
            var client = httpClientFactory.CreateClient("ApiClient");

            // Fetch categories
            var categoriesResponse = await client.GetAsync("http://localhost:5180/api/Category");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                var categoriesContent = await categoriesResponse.Content.ReadAsStringAsync();
                model.Categories = JsonSerializer.Deserialize<List<CategoryReturnDto>>(categoriesContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<CategoryReturnDto>();
            }

            // Fetch colors
            var colorsResponse = await client.GetAsync("http://localhost:5180/api/Color");
            if (colorsResponse.IsSuccessStatusCode)
            {
                var colorsContent = await colorsResponse.Content.ReadAsStringAsync();
                model.Colors = JsonSerializer.Deserialize<List<ColorReturnDto>>(colorsContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<ColorReturnDto>();
            }

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred while loading form data: {ex.Message}";
            return View(model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Reload categories and colors for the form
            try
            {
                var client = httpClientFactory.CreateClient("ApiClient");

                var categoriesResponse = await client.GetAsync("http://localhost:5180/api/Category");
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var categoriesContent = await categoriesResponse.Content.ReadAsStringAsync();
                    model.Categories = JsonSerializer.Deserialize<List<CategoryReturnDto>>(categoriesContent,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                }

                var colorsResponse = await client.GetAsync("http://localhost:5180/api/Color");
                if (colorsResponse.IsSuccessStatusCode)
                {
                    var colorsContent = await colorsResponse.Content.ReadAsStringAsync();
                    model.Colors = JsonSerializer.Deserialize<List<ColorReturnDto>>(colorsContent,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                }
            }
            catch
            {
            }

            return View(model);
        }

        try
        {
            var client = httpClientFactory.CreateClient("ApiClient");

            // Create the request body
            var productDto = new
            {
                name = model.Name,
                description = model.Description,
                price = model.Price,
                categoryId = model.CategoryId,
                colorsId = model.ColorsId
            };

            var jsonContent = JsonSerializer.Serialize(productDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send POST request to the API
            var response = await client.PostAsync("http://localhost:5180/api/Product", httpContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Product created successfully!";
                return RedirectToAction("Index");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                TempData["ErrorMessage"] = "You need to login to create a product.";
                return RedirectToAction("Login", "UiAccount");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Invalid data: {errorContent}");

                // Reload form data
                var categoriesResponse = await client.GetAsync("http://localhost:5180/api/Category");
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var categoriesContent = await categoriesResponse.Content.ReadAsStringAsync();
                    model.Categories = JsonSerializer.Deserialize<List<CategoryReturnDto>>(categoriesContent,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                }

                var colorsResponse = await client.GetAsync("http://localhost:5180/api/Color");
                if (colorsResponse.IsSuccessStatusCode)
                {
                    var colorsContent = await colorsResponse.Content.ReadAsStringAsync();
                    model.Colors = JsonSerializer.Deserialize<List<ColorReturnDto>>(colorsContent,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                }

                return View(model);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Failed to create product: {errorContent}");

                // Reload form data
                var categoriesResponse = await client.GetAsync("http://localhost:5180/api/Category");
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var categoriesContent = await categoriesResponse.Content.ReadAsStringAsync();
                    model.Categories = JsonSerializer.Deserialize<List<CategoryReturnDto>>(categoriesContent,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                }

                var colorsResponse = await client.GetAsync("http://localhost:5180/api/Color");
                if (colorsResponse.IsSuccessStatusCode)
                {
                    var colorsContent = await colorsResponse.Content.ReadAsStringAsync();
                    model.Colors = JsonSerializer.Deserialize<List<ColorReturnDto>>(colorsContent,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                }

                return View(model);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");

            // Reload form data
            try
            {
                var client = httpClientFactory.CreateClient("ApiClient");

                var categoriesResponse = await client.GetAsync("http://localhost:5180/api/Category");
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var categoriesContent = await categoriesResponse.Content.ReadAsStringAsync();
                    model.Categories = JsonSerializer.Deserialize<List<CategoryReturnDto>>(categoriesContent,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                }

                var colorsResponse = await client.GetAsync("http://localhost:5180/api/Color");
                if (colorsResponse.IsSuccessStatusCode)
                {
                    var colorsContent = await colorsResponse.Content.ReadAsStringAsync();
                    model.Colors = JsonSerializer.Deserialize<List<ColorReturnDto>>(colorsContent,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                }
            }
            catch
            {
            }

            return View(model);
        }
    }
}