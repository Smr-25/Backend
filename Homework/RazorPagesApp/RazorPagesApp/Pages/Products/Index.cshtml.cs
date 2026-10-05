


using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Data;
using RazorPagesApp.Models;

namespace RazorPagesApp.Pages.Products;

public class IndexModel(AppDbContext dbContext) : PageModel
{
    public List<Product> Products { get; set; }

    public async Task OnGet()
    {
        Products = await dbContext.Products.ToListAsync();
    }
}