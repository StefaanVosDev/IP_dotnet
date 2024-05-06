using BL.Domain.Questions;
using BL.Implementations;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers.api;

[Route("/api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IFlowManager _flowManager;
    private readonly IAnswerManager _answerManager;
    private readonly IAnalyticsManager _analyticsManager;

    public AnalyticsController(IFlowManager flowManager, IAnswerManager answerManager, IAnalyticsManager analyticsManager)
    {
        _flowManager = flowManager;
        _answerManager = answerManager;
        _analyticsManager = analyticsManager;
    }
    
    //TODO change to retrieve 1 questions answers at a time for performace
   [HttpGet("GetFlowAnalytics/{flowId}")]
    public async Task<IActionResult> GetFlowAnalytics(int flowId)
{
    try
    {
        var questions = await _flowManager.GetQuestionsByFlowIdAsync(flowId);
        var chartData = new List<object>();
        var openQuestions = new List<object>();

        foreach (var question in questions)
        {
            var answers = await _answerManager.GetAnswersByQuestionIdAsync(question.Id);
            switch (question.Type)
            {
                case QuestionType.SingleChoice:
                    if (question is SingleChoiceQuestion singleChoiceQuestion)
                    {
                        var data = _analyticsManager.GetSingleChoiceQuestionData(singleChoiceQuestion, answers);
                        chartData.Add(data);
                    }
                    break;
                case QuestionType.MultipleChoice:
                    if (question is MultipleChoiceQuestion multipleChoiceQuestion)
                    {
                        var data = _analyticsManager.GetMultipleChoiceQuestionData(multipleChoiceQuestion, answers);
                        chartData.Add(data);
                    }
                    break;
                case QuestionType.Range:
                    if (question is RangeQuestion rangeQuestion)
                    {
                        var data = _analyticsManager.GetRangeQuestionData(rangeQuestion, answers);
                        chartData.Add(data);
                    }
                    break;
                case QuestionType.Open:
                    if (question is OpenQuestion openQuestion)
                    {
                        var data = _analyticsManager.GetOpenQuestionData(openQuestion, answers);
                        openQuestions.Add(data); // Add open questions to the new list
                    }
                    break;

                default:
                    Console.WriteLine($"Unexpected question type for question ID: {question.Id}");
                    break;
            }
        }

        var jsonResult = new JsonResult(new { chartData, openQuestions })
        {
            ContentType = "application/json"
        };
        return jsonResult;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
    }
}
}