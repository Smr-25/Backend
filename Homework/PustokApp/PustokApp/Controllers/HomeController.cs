using Microsoft.AspNetCore.Mvc;
using PustokApp.Data;
using PustokApp.ViewModels;

namespace PustokApp.Controllers
{
    public class HomeController(AppDbContext dbContext) : Controller
    {
        public IActionResult Index()
        {
            HomeVm homeVm = new HomeVm
            {
                Sliders = dbContext.Sliders.ToList()
            };
            return View(homeVm);
        }
    }
}