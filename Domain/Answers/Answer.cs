using BL.Domain.Questions;

namespace BL.Domain.Answers;

public class Answer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    
    public Answer()
    {
    }
    
    public Answer(Question question)
    {
        Question = question;
    }
}