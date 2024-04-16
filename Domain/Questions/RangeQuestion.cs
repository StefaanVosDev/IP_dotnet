namespace BL.Domain.Questions;

public class RangeQuestion : Question
{
    public int Min { get; set; }
    public int Max { get; set; }
    
    public RangeQuestion()
    {
    }
    
    public RangeQuestion(string text, int min, int max) : base(text, QuestionType.Range)
    {
        Min = min;
        Max = max;
    }
}