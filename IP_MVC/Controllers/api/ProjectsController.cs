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

    public ProjectsController(IProjectManager projectManager)
    {
        _projectManager = projectManager;
    }
    
    
    [HttpPut("{projectId}")]
    public async Task<IActionResult> Change(int projectId, [FromBody] ProjectEditDto updateDto)
    {
        if (updateDto == null)
        {
            return BadRequest("Invalid project data.");
        }
        
        var project = await _projectManager.FindByIdAsync(projectId);
        
        if (project == null)
        {
            return NotFound($"Project with ID {updateDto.ProjectId} not found.");
        }

        Project newProject = new Project
        {
            Name = updateDto.NewName,
            Description = updateDto.NewDescription,
            AdminId = updateDto.AdminId
        };
        
        newProject.AdminId = project.AdminId;
        project.Name = updateDto.NewName;
        project.Description = updateDto.NewDescription;
        
        await _projectManager.UpdateAsync(project, newProject);
        
        return Ok(project); 
    }
}