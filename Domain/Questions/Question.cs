using Microsoft.AspNetCore.Identity;

namespace BL.Domain.Questions;

public class Question
{
    //TODO add content
    public int Id { get; set; }
    public int Position { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
    public Media Media { get; set; }
    public int FlowId { get; set; }

    public Question()
    {
    }

    public Question(int position, string text, QuestionType type, Media media)
    {
        Position = position;
        Text = text;
        Type = type;
        Media = media;
    }
}
