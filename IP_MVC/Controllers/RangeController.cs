using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers;

public class RangeController : Controller
{
    public IActionResult Index()
    {
        return View("~/Views/Respondent/Questions/Range/Index.cshtml");
    }
}