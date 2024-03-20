using BL.Domain;
using BL.Domain.Questions;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IP_MVC.Controllers;

public class FlowController : Controller
{
    private readonly IFlowManager _flowManager;
    
    public FlowController(IFlowManager flowManager)
    {
        _flowManager = flowManager;
    }
    
    // GET
    public IActionResult Flow(int id)
    {
        var Flows = _flowManager.GetParentFlowsByProjectId(id);
        return View(Flows);
    }
    
    // GET
    public IActionResult SubFlow(int id)
    {
        var subFlows = _flowManager.GetFlowsByParentId(id);
        return View(subFlows);
    }
    
    // GET
    public IActionResult StartFlow(int id)
    {
        var firstSubFlow = _flowManager.GetFirstSubFlowByParentId(id);
        return RedirectToAction("PlaySubFlow", new { id = firstSubFlow.Id });
    }
    
    // GET
    public IActionResult PlaySubFlow(int id)
    {
        // Get the current question index from the session
        var questionIndex = HttpContext.Session.GetInt32("questionIndex") ?? 0;

        // Get the questions for the subflow and convert to a list
        var questions = _flowManager.GetQuestionsByFlowId(id).ToList();

        // Check if there are any more questions
        if (questionIndex >= questions.Count)
        {
            // No more questions, end the subflow
            HttpContext.Session.Remove("questionIndex");
            return RedirectToAction("EndSubFlow");
        }

        // Get the current question
        var question = questions[questionIndex];

        // Increment the question index for the next request
        HttpContext.Session.SetInt32("questionIndex", questionIndex + 1);

        // Pass the question index to the view
        ViewData["QuestionNumber"] = questionIndex + 1;

        // Check the type of the question and return the corresponding view
        return question.Type switch
        {
            QuestionType.MultipleChoice => View("Questions/MultipleChoice", question as MultipleChoiceQuestion),
            QuestionType.Open => View("Questions/Open", question as OpenQuestion),
            QuestionType.Range => View("Questions/Range", question as RangeQuestion),
            QuestionType.SingleChoice => View("Questions/SingleChoice", question as SingleChoiceQuestion),
            _ => throw new Exception("Unknown question type")
        };
    }
    public IActionResult EndSubFlow()
    {
        ViewData["Message"] = "Thank you for completing the subflow!";
        return View(EndSubFlow);
    }
}