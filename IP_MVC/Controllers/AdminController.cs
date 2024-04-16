using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers;

public class AdminController : Controller
{
    // GET
    [Authorize(Roles = CustomIdentityConstants.AdminRole)]
    public IActionResult Index()
    {
        return View();
    }
}