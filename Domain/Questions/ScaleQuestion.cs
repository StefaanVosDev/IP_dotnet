namespace BL.Domain.Questions;

public class ScaleQuestion(string text, int min, int max) : Question(text)
{
    public int Min { get; set; } = min;
    public int Max { get; set; } = max;
}