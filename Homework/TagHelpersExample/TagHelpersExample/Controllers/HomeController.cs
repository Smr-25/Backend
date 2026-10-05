using Microsoft.AspNetCore.Mvc;
using TagHelpersExample.Models;


namespace TagHelpersExample.Controllers;

public class HomeController : Controller
{
    private List<Marka> _markas = new List<Marka>()
    {
        new Marka() { Id = 1, Name = "BMW" },
        new Marka() { Id = 2, Name = "Mercedes" },
        new Marka() { Id = 3, Name = "Audi" },
        new Marka() { Id = 4, Name = "Toyota" },
        new Marka() { Id = 5, Name = "Honda" }

    };
    public IActionResult Index()
    {
        return View(_markas);
    }
}