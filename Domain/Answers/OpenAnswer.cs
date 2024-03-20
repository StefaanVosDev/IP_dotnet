using BL.Domain.Questions;

namespace BL.Domain.Answers;

public class OpenAnswer : Answer
{
    public string Text { get; set; }
    
    public OpenAnswer()
    {
    }
    
    public OpenAnswer(Question question, string text) : base(question)
    {
        Text = text;
    }
}