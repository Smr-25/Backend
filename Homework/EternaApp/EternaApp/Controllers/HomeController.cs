using System.Diagnostics;
using EternaApp.Data;
using Microsoft.AspNetCore.Mvc;
using EternaApp.Models;
using EternaApp.ViewModels;

namespace EternaApp.Controllers;

public class HomeController(EternaDbContext dbContext) : Controller
{

    public IActionResult Index()
    {
        HomeVm homeVm = new()
        {
            Sliders = dbContext.Sliders.ToList(),
            Services =  dbContext.Services.ToList(),
            Features = dbContext.Features.ToList()
        };
        return View(homeVm);
    }

  
}