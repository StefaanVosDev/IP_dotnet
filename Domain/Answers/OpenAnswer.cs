using BL.Domain.Questions;

namespace BL.Domain.Answers;

public class OpenAnswer(Question question) : Answer(question)
{
    public string Text { get; set; }
}