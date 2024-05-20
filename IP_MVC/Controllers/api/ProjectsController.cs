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
    
    
    [HttpPut]
    public async Task<IActionResult> Change([FromBody] ProjectEditDto updateDto)
    {
        if (updateDto == null)
        {
            return BadRequest("Invalid project data.");
        }
        
        var project = await _projectManager.FindByIdAsync(updateDto.ProjectId);
        
        Project updatedProject = new Project
        {
            Id = updateDto.ProjectId,
            Name = updateDto.NewName,
            Description = updateDto.NewDescription,
            AdminId = updateDto.AdminId
        };
        
        
        await _projectManager.UpdateAsync( project, updatedProject);

        var returnedProject = _projectManager.FindByIdAsync(project.Id) ;
    
        return Ok(returnedProject); 
    }
} 