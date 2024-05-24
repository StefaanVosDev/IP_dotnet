using BL.Domain;
using BL.Interfaces;
using IP_MVC.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Project = BL.Domain.Project;

namespace IP_MVC.Controllers.api;

[ApiController]
[Route("api/[controller]")]
public class FlowsController : ControllerBase    
{
    private readonly IFlowManager _flowManager;

    public FlowsController(IFlowManager flowManager)
    {
        _flowManager = flowManager;
    }

    [HttpGet("{projectId}")]
    public Task<IActionResult> GetFlow(int projectId)
    {
        var project = _flowManager.GetFlowById(projectId);

        if (project == null)
        {
            return Task.FromResult<IActionResult>(NotFound());
        }

        return Task.FromResult<IActionResult>(Ok(project));
    }
    
    [HttpPut]
    public async Task<IActionResult> Change([FromBody] FlowEditDto updateDto)
    {
        
        if (updateDto == null)
        {
            return BadRequest("Invalid flow data.");
        }
        
        var flow =  _flowManager.GetFlowById(updateDto.Id);

        var updatedFlow = flow;
        updatedFlow.Name = updateDto.NewName;
        updatedFlow.Description = updateDto.NewDescription;
        
        await _flowManager.UpdateAsync( flow, updatedFlow);
        return NoContent(); 
    }
}