using BL.Implementations;
using BL.Interfaces;
using IP_MVC.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Project = BL.Domain.Project;

namespace IP_MVC.Controllers.api;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectManager _projectManager;
    private readonly UnitOfWork _unitOfWork;

    public ProjectsController(IProjectManager projectManager, UnitOfWork unitOfWork)
    {
        _projectManager = projectManager;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _projectManager.FindByIdAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }
    
    [HttpPut]
    public async Task<IActionResult> Change([FromBody] ProjectEditDto updateDto)
    {
        _unitOfWork.BeginTransaction();
        if (updateDto == null)
        {
            return BadRequest("Invalid project data.");
        }
          
        var project = await _projectManager.FindByIdAsync(updateDto.ProjectId);

        var updatedProject = project;
        updatedProject.Name = updateDto.NewName;
        updatedProject.Description = updateDto.NewDescription;
        updatedProject.AdminId = updateDto.AdminId;
        
        await _projectManager.UpdateAsync( project, updatedProject);

        _unitOfWork.Commit();
        return NoContent();
    }
} 