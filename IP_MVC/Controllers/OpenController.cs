using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers;

public class OpenController : Controller
{
    public IActionResult Index()
    {
        return View("~/Views/Respondent/Questions/Open/Index.cshtml");
    }
}