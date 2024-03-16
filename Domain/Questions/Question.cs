namespace BL.Domain.Questions;

public abstract class Question(string text)
{
    //TODO add content
    public int Id { get; set; }
    public string Text { get; set; } = text;
}