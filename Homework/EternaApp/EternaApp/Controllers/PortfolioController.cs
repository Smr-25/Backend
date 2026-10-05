using EternaApp.Data;
using EternaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EternaApp.Controllers;

public class PortfolioController(EternaDbContext dbContext) : Controller
{
    public IActionResult Index()
    {
        PortfolioVm portfolioVm = new()
        {
            Portfolios = dbContext.Portfolios.Include(p => p.PortfolioImages).ToList(),
            Categories = dbContext.Categories.ToList(),
        };
        return View(portfolioVm);
    }

    public IActionResult Detail(int? id)
    {
        if (id == null) return BadRequest();
        var portfolio = dbContext.Portfolios
            .Include(p => p.PortfolioImages)
            .Include(p => p.Category)
            .FirstOrDefault(p => p.Id == id);


        if (portfolio == null) return NotFound();

        return View(portfolio);
    }
}