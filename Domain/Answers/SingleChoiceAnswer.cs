using BL.Domain.Questions;

namespace BL.Domain.Answers;

public class SingleChoiceAnswer(Question question) : Answer(question)
{
    public string SelectedOption { get; set; }
}