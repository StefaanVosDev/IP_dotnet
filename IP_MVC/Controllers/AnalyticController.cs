using BL.Domain;
using BL.Interfaces;
using IP_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers;

public class AnalyticController(IProjectManager projectManager, IAnswerManager answerManager) : Controller
{
    [HttpGet]
    public IActionResult Analytic(int projectId = 1)
    {
        var flows = projectManager.GetFlowsByProjectId(projectId);
        return View(flows);
    }
}