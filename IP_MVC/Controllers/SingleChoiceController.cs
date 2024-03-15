using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers;

public class SingleChoiceController : Controller
{
    public IActionResult Index()
    {
        return View("~/Views/Respondent/Questions/SingleChoice/Index.cshtml");
    }
}