using System.Security.Claims;
using BL.Domain;
using BL.Implementations;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers;

public class ProjectController : Controller
{
    private readonly IProjectManager _projectManager;

    public ProjectController(IProjectManager projectManager)
    {
        _projectManager = projectManager;
    }

    public IActionResult Project()
    {
        var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var projects = _projectManager.GetProjectsByAdminId(adminId);
        return View(projects);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var project = await _projectManager.FindByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Project newProject)
    {
        if (!ModelState.IsValid) return View(newProject);

        var existingProject = await _projectManager.FindByIdAsync(id);
        if (existingProject == null)
        {
            return NotFound();
        }

        // Ensure the AdminId remains the same
        newProject.AdminId = existingProject.AdminId;

        await _projectManager.UpdateAsync(existingProject, newProject);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var project = await _projectManager.FindByIdAsync(id);
        await _projectManager.DeleteAsync(project);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Project project)
    {
        if (!ModelState.IsValid) return View(project);

        // Assign the UserId of the project to the current user's Id
        project.AdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _projectManager.AddAsync(project);
        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        throw new NotImplementedException();
    }
}