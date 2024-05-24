using BL.Domain.Answers;
using BL.Domain.Questions;
using BL.Interfaces;

namespace BL.Implementations;

public class AnalyticsManager : IAnalyticsManager
{
    public object GetSingleChoiceQuestionData(SingleChoiceQuestion question, IEnumerable<Answer> answers)
    {
        var answerGroups = answers.GroupBy(a => a.AnswerText);
        var labels = question.Options;
        var data = answerGroups.Select(g => g.Count()).ToList();

        return new
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
                        hoverOffset = 4
                    }
                }
            }
        };
    }

    public object GetMultipleChoiceQuestionData(MultipleChoiceQuestion question, IEnumerable<Answer> answers)
    {
        var answerGroups = answers.SelectMany(a => a.AnswerText.Split(';')).GroupBy(a => a);
        var labels = question.Options;
        var data = answerGroups.Select(g => g.Count()).ToList();

        return new
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
        };
    }
    
    public object GetRangeQuestionData(RangeQuestion question, IEnumerable<Answer> answers)
    {
        var answerValues = answers.Select(a => int.Parse(a.AnswerText)).ToList();

        var min = question.Min;
        var max = question.Max;

        var ranges = new List<string>();
        for (var i = min; i < max; i++)
        {
            ranges.Add($"{i}-{i + 1}");
        }

        var data = ranges.Select(range => range.Split('-').Select(int.Parse).ToList())
            .Select(bounds =>
                answerValues.Count(value => value >= bounds[0] && value < bounds[1]))
            .ToList();

        return new
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
        };
    }

    public object GetOpenQuestionData(OpenQuestion question, IEnumerable<Answer> answers)
    {
        return new
        {
            question = question.Text,
            answers = answers.Select(a => a.AnswerText).ToList()
        };
    }
}