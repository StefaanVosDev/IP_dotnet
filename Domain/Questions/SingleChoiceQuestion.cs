namespace BL.Domain.Questions;

public class SingleChoiceQuestion(string text, List<string> options) : Question(text)
{
    public List<string> Options { get; set; } = options;
}