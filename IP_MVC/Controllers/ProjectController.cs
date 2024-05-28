using System.Security.Claims;
using BL.Domain;
using BL.Implementations;
using BL.Interfaces;
using IP_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers;

public class ProjectController : Controller
{
    private readonly IProjectManager _projectManager;
    private readonly UnitOfWork _unitOfWork;

    public ProjectController(IProjectManager projectManager, UnitOfWork unitOfWork)
    {
        _projectManager = projectManager;
        _unitOfWork = unitOfWork;
    }


    public async Task<IActionResult> ProjectDashboard()
    {
        ViewBag.Dashboard = true;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var adminProjects = await _projectManager.GetProjectsByAdminIdAsync(userId);
        var facilitatorProjects = _projectManager.GetProjectsByFacilitatorId(userId);

        var viewModel = new ProjectDashboardViewModel
        {
            AdminProjects = adminProjects,
            FacilitatorProjects = facilitatorProjects
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Delete(int parentFlowId)
    {
        _unitOfWork.BeginTransaction();
        var project = await _projectManager.FindByIdAsync(parentFlowId);
        await _projectManager.DeleteAsync(project);

        _unitOfWork.Commit();
        return RedirectToAction("ProjectDashboard");
    }

    [HttpPost]
    public async Task<IActionResult> Create(Project project)
    {
        _unitOfWork.BeginTransaction();
        if (!ModelState.IsValid) return RedirectToAction("ProjectDashboard");

        project.AdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _projectManager.AddAsync(project);

        _unitOfWork.Commit();
        return RedirectToAction("ProjectDashboard");
    }


    [HttpGet]
    public IActionResult ManageFacilitators(int projectId)
    {
        var facilitators = _projectManager.GetFacilitatorsByProjectId(projectId);
        
        var model = new ManageFacilitatorsViewModel
        {
            Facilitators = facilitators,
            ProjectId = projectId
        };

        return View(model);
    }


    
}