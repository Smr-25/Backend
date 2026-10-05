using Microsoft.AspNetCore.Mvc;
using TagHelpersExample.Models;

namespace TagHelpersExample.Controllers;

public class MadelController : Controller
{
    List<Madel> _madels = new List<Madel>()
    {
        new Madel() { Id = 1, Name = "X5", MarkaId = 1 },
        new Madel() { Id = 2, Name = "C-Class", MarkaId = 2 },
        new Madel() { Id = 3, Name = "A6", MarkaId = 3 },
        new Madel() { Id = 4, Name = "Camry", MarkaId = 4 },
        new Madel() { Id = 5, Name = "Civic", MarkaId = 5 }
    };
    public IActionResult Index(int? markaId)
    {
        if(markaId is null)
            return View(_madels);
        var filteredMadels = _madels.Where(m => m.MarkaId == markaId).ToList();
        return View(filteredMadels);
    }
}