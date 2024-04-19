using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BL.Interfaces;

namespace IP_MVC.Controllers;

public class AdminController : Controller
{
    private readonly IProjectManager _projectManager;
    public AdminController(IProjectManager projectManager)
    {
        _projectManager = projectManager;
    }
    
    // GET
    [Authorize(Roles = CustomIdentityConstants.AdminRole)]
    public IActionResult Index()
    {
        var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var projects = _projectManager.GetProjectsByAdminId(adminId);
        return View(projects);
    }
}