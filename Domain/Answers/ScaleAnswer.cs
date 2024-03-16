using BL.Domain.Questions;

namespace BL.Domain.Answers;

public class ScaleAnswer(Question question) : Answer(question)
{
    public int SelectedValue { get; set; }
}