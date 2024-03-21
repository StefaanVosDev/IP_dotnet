namespace BL.Domain.Questions;

public class Question
{
    //TODO add content
    public int Id { get; set; }
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public int FlowId { get; set; }

    public Question()
    {
    }

    public Question(string text, QuestionType type)
    {
        Text = text;
        Type = type;
    }
}
