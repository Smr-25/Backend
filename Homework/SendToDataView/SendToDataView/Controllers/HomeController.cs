using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SendToDataView.Models;

namespace SendToDataView.Controllers;

public class HomeController : Controller
{

    Student _student = new Student()
    {
        Id = 1,
        Name = "John Doe",
        Age = 20
    };
    public IActionResult Index()
    {
        ViewData["StudentId"]= _student.Id;
        ViewBag.StudentName = _student.Name;
        TempData["StudentAge"] = _student.Age;

        return RedirectToAction("About");
    }

    public IActionResult About()
    {
        
        return View();
    }

   
}