using Microsoft.AspNetCore.Mvc;
using TagHelpersExample.Models;

namespace TagHelpersExample.Controllers;

public class CarController : Controller
{
    private List<Car> _cars = new List<Car>()
    {
        new Car() { Id = 1, Name = "Car1", MadelId = 1 },
        new Car() { Id = 2, Name = "Car2", MadelId = 2 },
        new Car() { Id = 3, Name = "Car3", MadelId = 3 },
        new Car() { Id = 4, Name = "Car4", MadelId = 4 },
        new Car() { Id = 5, Name = "Car5", MadelId = 5 }
    };

    public IActionResult Index(int? madelId)
    {
        if (madelId is null)
            return View(_cars);
        var filteredCars = _cars.Where(c => c.MadelId == madelId).ToList();
        return View(filteredCars);
    }
    
    public IActionResult Detail(int? id)
    {
        if (id is null)
            return NotFound();
        var car = _cars.FirstOrDefault(c => c.Id == id);
        if (car is null)
            return NotFound();
        return View(car);
    }
}