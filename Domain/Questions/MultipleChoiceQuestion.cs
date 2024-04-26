namespace BL.Domain.Questions;

public class MultipleChoiceQuestion : Question
{
    public List<string> Options { get; set; }
    
    public MultipleChoiceQuestion()
    {
    }

    public MultipleChoiceQuestion(int position, string text, List<string> options, Media media) : base(position, text, QuestionType.MultipleChoice, media)
    {
        Options = options;
    }
}