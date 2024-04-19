namespace BL.Domain.Questions;

public class OpenQuestion : Question
{
    public OpenQuestion()
    {
    }

    public OpenQuestion(string text, Media media) : base(text, QuestionType.Open, media)
    {
    }
    
}