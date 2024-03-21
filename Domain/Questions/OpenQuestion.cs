namespace BL.Domain.Questions;

public class OpenQuestion : Question
{
    public OpenQuestion()
    {
    }

    public OpenQuestion(string text) : base(text, QuestionType.Open)
    {
    }
    
}