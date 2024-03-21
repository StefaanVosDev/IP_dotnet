namespace BL.Domain.Questions;

public class MultipleChoiceQuestion : Question
{
    public List<string> Options { get; set; }
    
    public MultipleChoiceQuestion()
    {
    }

    public MultipleChoiceQuestion(string text, List<string> options) : base(text, QuestionType.MultipleChoice)
    {
        Options = options;
    }
}