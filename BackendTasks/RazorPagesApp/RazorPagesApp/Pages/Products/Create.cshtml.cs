

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesApp.Data;
using RazorPagesApp.Models;

namespace RazorPagesApp.Pages.Products;

public class CreateModel(AppDbContext dbContext) : PageModel
{
    [BindProperty]
    public Product Product { get; set; }
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        dbContext.Products.Add(Product);
        dbContext.SaveChanges();
        return RedirectToPage("Index");
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage("Index");
    }

    public IActionResult OnPostCreate2()
    {
        Product.Price = 12;
        dbContext.Products.Add(Product);
        dbContext.SaveChanges();
        return RedirectToPage("Index");
    }
}