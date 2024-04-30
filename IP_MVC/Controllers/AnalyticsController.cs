// AnalyticsController.cs

using BL.Domain;
using BL.Interfaces;
using IP_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers;

public class AnalyticsController(IProjectManager projectManager, IAnswerManager answerManager) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Analytics(int projectId)
    {
        var project = await projectManager.GetAsync(projectId);
        if (project == null)
        {
            return NotFound();
        }

        var model = new ProjectAnalyticsViewModel
        {
            ProjectId = projectId,
            OpenQuestionData = GetOpenQuestionData(project),
            MultipleChoiceQuestionData = GetMultipleChoiceQuestionData(project),
            SingleChoiceQuestionData = GetSingleChoiceQuestionData(project),
            RangeQuestionData = GetRangeQuestionData(project)
        };

        return View(model);
    }

    private Dictionary<string, int> GetOpenQuestionData(Project project)
    {
        // Retrieve the open question answers from the project
        var answers = answerManager.GetOpenQuestionAnswersByProjectId(project.Id);

        // Prepare the data for the word cloud
        var dataForChart = new Dictionary<string, int>();

        foreach (var answer in answers)
        {
            var words = answer.Text.Split(' ');

            foreach (var word in words)
            {
                if (dataForChart.ContainsKey(word))
                {
                    dataForChart[word]++;
                }
                else
                {
                    dataForChart[word] = 1;
                }
            }
        }

        return dataForChart;
    }

    private Dictionary<string, int> GetMultipleChoiceQuestionData(Project project)
    {
        // Retrieve the multiple choice question answers from the project
        var answers = answerManager.GetMultipleChoiceQuestionAnswersByProjectId(project.Id);

        // Prepare the data for the bar chart
        var dataForChart = new Dictionary<string, int>();

        foreach (var answer in answers)
        {
            if (dataForChart.ContainsKey(answer.Choice))
            {
                dataForChart[answer.Choice]++;
            }
            else
            {
                dataForChart[answer.Choice] = 1;
            }
        }

        return dataForChart;
    }

    private Dictionary<string, int> GetSingleChoiceQuestionData(Project project)
    {
        // Retrieve the single choice question answers from the project
        var answers = answerManager.GetSingleChoiceQuestionAnswersByProjectId(project.Id);

        // Prepare the data for the pie chart
        var dataForChart = new Dictionary<string, int>();

        foreach (var answer in answers)
        {
            if (dataForChart.ContainsKey(answer.Choice))
            {
                dataForChart[answer.Choice]++;
            }
            else
            {
                dataForChart[answer.Choice] = 1;
            }
        }

        return dataForChart;
    }

    private Dictionary<string, int> GetRangeQuestionData(Project project)
    {
        // Retrieve the range question answers from the project
        var answers = answerManager.GetRangeQuestionAnswersByProjectId(project.Id);

        // Prepare the data for the histogram
        var dataForChart = new Dictionary<string, int>();

        foreach (var answer in answers)
        {
            var range = $"{answer.MinRange}-{answer.MaxRange}";

            if (dataForChart.ContainsKey(range))
            {
                dataForChart[range]++;
            }
            else
            {
                dataForChart[range] = 1;
            }
        }

        return dataForChart;
    }
}