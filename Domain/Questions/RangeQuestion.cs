namespace BL.Domain.Questions;

public class ScaleQuestion : Question
{
    //TODO change name to range
    public int Min { get; set; }
    public int Max { get; set; }
    
    public ScaleQuestion()
    {
    }
    
    public ScaleQuestion(string text, int min, int max) : base(text)
    {
        Min = min;
        Max = max;
    }
}