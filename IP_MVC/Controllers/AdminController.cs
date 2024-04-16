using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}