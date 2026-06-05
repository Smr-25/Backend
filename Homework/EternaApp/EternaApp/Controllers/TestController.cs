using EternaApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EternaApp.Controllers;

public class TestController(BankService bankService,BankManager bankManager) : Controller
{
    public IActionResult Index()
    {
        bankService.Add();
        bankManager.AddBalance();
        return Content($"BankService {bankManager.BankManagerBalance}");
    }
}