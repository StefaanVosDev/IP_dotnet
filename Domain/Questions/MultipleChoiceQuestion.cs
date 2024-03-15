namespace BL.Domain.Questions;

public class MultipleChoiceQuestion(string text, List<string> options) : Question(text)
{
    public List<string> Options { get; set; } = options;
}