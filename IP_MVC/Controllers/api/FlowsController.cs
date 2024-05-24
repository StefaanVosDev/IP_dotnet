using BL.Domain;
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
public class FlowsController : ControllerBase    
{
    private readonly IFlowManager _flowManager;
    private readonly UnitOfWork _unitOfWork;

    public FlowsController(IFlowManager flowManager, UnitOfWork unitOfWork)
    {
        _flowManager = flowManager;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{flowId}")]
    public Task<IActionResult> GetFlow(int flowId)
    {
        var flow = _flowManager.GetFlowById(flowId);

        if (flow == null)
        {
            return Task.FromResult<IActionResult>(NotFound());
        }

        return Task.FromResult<IActionResult>(Ok(flow));
    }
    
    [HttpPut]
    public async Task<IActionResult> Change([FromBody] FlowEditDto updateDto)
    {
        _unitOfWork.BeginTransaction();
        if (updateDto == null)
        {
            return BadRequest("Invalid flow data.");
        }
        
        var flow =  _flowManager.GetFlowById(updateDto.Id);

        var updatedFlow = flow;
        updatedFlow.Name = updateDto.NewName;
        updatedFlow.Description = updateDto.NewDescription;
        
        await _flowManager.UpdateAsync( flow, updatedFlow);
        _unitOfWork.Commit();
        return NoContent(); 
    }
}