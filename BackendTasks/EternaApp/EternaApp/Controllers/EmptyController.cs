using EternaApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EternaApp.Controllers;

public class EmptyController(BankService bankService) : Controller
{
    public IActionResult Index()
    {
        bankService.Add();
        return Content($"Balance: {bankService.Balance}");
    }
}