using BL.Domain.Questions;

namespace BL.Domain.Answers;

public class MultipleChoiceAnswer(Question question) : Answer(question)
{
    public List<string> SelectedOptions { get; set; }
}