using BL.Domain.Questions;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CherubNLP;
using NumSharp;

namespace IP_MVC.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IFlowManager _flowManager;
        private readonly IAnswerManager _answerManager;

        public AnalyticsController(IFlowManager flowManager, IAnswerManager answerManager)
        {
            _flowManager = flowManager;
            _answerManager = answerManager;
        }

        [HttpGet("GetFlowAnalytics/{flowId}")]
        public async Task<IActionResult> GetFlowAnalytics(int flowId)
        {
            var questions = await _flowManager.GetQuestionsByFlowIdAsync(flowId);
            var chartData = new List<object>();
            foreach (var question in questions)
            {
                var answers = await _answerManager.GetAnswersByQuestionIdAsync(question.Id);
                switch (question.Type)
                {
                    case QuestionType.SingleChoice:
                        if (question is SingleChoiceQuestion singleChoiceQuestion)
                        {
                            var answerGroups = answers.GroupBy(a => a.AnswerText);
                            var labels = singleChoiceQuestion.Options;
                            var data = answerGroups.Select(g => g.Count()).ToList();

                            chartData.Add(new
                            {
                                type = "doughnut",
                                data = new
                                {
                                    labels,
                                    datasets = new[]
                                    {
                                        new
                                        {
                                            label = question.Text,
                                            data,
                                            backgroundColor = new[]
                                                { "rgb(255, 99, 132)", "rgb(54, 162, 235)", "rgb(255, 205, 86)" },
                                            hoverOffset = 4
                                        }
                                    }
                                }
                            });
                        }

                        break;
                    case QuestionType.MultipleChoice:
                        if (question is MultipleChoiceQuestion multipleChoiceQuestion)
                        {
                            var answerGroups = answers.GroupBy(a => a.AnswerText);
                            var labels = multipleChoiceQuestion.Options;
                            var data = answerGroups.Select(g => g.Count()).ToList();

                            chartData.Add(new
                            {
                                type = "bar",
                                data = new
                                {
                                    labels,
                                    datasets = new[]
                                    {
                                        new
                                        {
                                            label = question.Text,
                                            data,
                                            backgroundColor = new[]
                                            {
                                                "rgba(255, 99, 132, 0.2)",
                                                "rgba(255, 159, 64, 0.2)",
                                                "rgba(255, 205, 86, 0.2)",
                                                "rgba(75, 192, 192, 0.2)",
                                                "rgba(54, 162, 235, 0.2)",
                                                "rgba(153, 102, 255, 0.2)",
                                                "rgba(201, 203, 207, 0.2)"
                                            },
                                            borderColor = new[]
                                            {
                                                "rgb(255, 99, 132)",
                                                "rgb(255, 159, 64)",
                                                "rgb(255, 205, 86)",
                                                "rgb(75, 192, 192)",
                                                "rgb(54, 162, 235)",
                                                "rgb(153, 102, 255)",
                                                "rgb(201, 203, 207)"
                                            },
                                            borderWidth = 1
                                        }
                                    }
                                },
                                options = new
                                {
                                    scales = new
                                    {
                                        y = new
                                        {
                                            beginAtZero = true
                                        }
                                    }
                                }
                            });
                        }

                        break;
                    case QuestionType.Open:
                        if (question is OpenQuestion openQuestion)
                        {
                            var answersText = answers.Select(a => a.AnswerText).ToList();

                            // Use CherubNLP to compute the similarity of the answers to each other.
                            var similarities = new List<double>();
                            for (int i = 0; i < answersText.Count; i++)
                            {
                                for (int j = i + 1; j < answersText.Count; j++)
                                {
                                    var similarity = Similarity.Cosine(answersText[i], new[] { answersText[j] },
                                        "dbpedia.ftz");
                                    similarities.Add(similarity.Max());
                                }
                            }

                            var averageSimilarity = similarities.Average();

                            chartData.Add(new
                            {
                                question = openQuestion.Text,
                                averageSimilarity
                            });
                        }

                        break;
                    case QuestionType.Range:
                        if (question is RangeQuestion rangeQuestion)
                        {
                            var answerValues = answers.Select(a => int.Parse(a.AnswerText)).ToList();

                            var min = rangeQuestion.Min;
                            var max = rangeQuestion.Max;

                            var ranges = new List<string>();
                            for (var i = min; i < max; i++)
                            {
                                ranges.Add($"{i}-{i + 1}");
                            }

                            var data = ranges.Select(range => range.Split('-').Select(int.Parse).ToList())
                                .Select(bounds => answerValues.Count(value => value >= bounds[0] && value < bounds[1]))
                                .ToList();

                            chartData.Add(new
                            {
                                type = "bar",
                                data = new
                                {
                                    labels = ranges,
                                    datasets = new[]
                                    {
                                        new
                                        {
                                            label = question.Text,
                                            data,
                                            backgroundColor = new[]
                                            {
                                                "rgba(255, 99, 132, 0.2)",
                                                "rgba(255, 159, 64, 0.2)",
                                                "rgba(255, 205, 86, 0.2)",
                                                "rgba(75, 192, 192, 0.2)",
                                                "rgba(54, 162, 235, 0.2)",
                                                "rgba(153, 102, 255, 0.2)",
                                                "rgba(201, 203, 207, 0.2)"
                                            },
                                            borderColor = new[]
                                            {
                                                "rgb(255, 99, 132)",
                                                "rgb(255, 159, 64)",
                                                "rgb(255, 205, 86)",
                                                "rgb(75, 192, 192)",
                                                "rgb(54, 162, 235)",
                                                "rgb(153, 102, 255)",
                                                "rgb(201, 203, 207)"
                                            },
                                            borderWidth = 1
                                        }
                                    }
                                },
                                options = new
                                {
                                    scales = new
                                    {
                                        y = new
                                        {
                                            beginAtZero = true
                                        }
                                    }
                                }
                            });
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            var jsonResult = new JsonResult(chartData)
            {
                ContentType = "application/json"
            };
            return jsonResult;
        }
    }
}