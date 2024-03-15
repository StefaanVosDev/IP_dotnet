using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers;

public class MultipleChoiceController : Controller
{
    public IActionResult Index()
    {
        return View("~/Views/Respondent/Questions/MultipleChoice/Index.cshtml");
    }
}