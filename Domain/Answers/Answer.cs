using BL.Domain.Questions;

namespace BL.Domain.Answers;

public abstract class Answer(Question question)
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; } = question;
}