using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers;

public class FacilitatorController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}