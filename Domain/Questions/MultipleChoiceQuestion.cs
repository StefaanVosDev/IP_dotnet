namespace BL.Domain.Questions;

public class MultipleChoiceQuestion : Question
{
    public List<string> Options { get; set; }
    
    public MultipleChoiceQuestion()
    {
    }

    public MultipleChoiceQuestion(string text, List<string> options, Media media) : base(text, QuestionType.MultipleChoice, media)
    {
        Options = options;
    }
}