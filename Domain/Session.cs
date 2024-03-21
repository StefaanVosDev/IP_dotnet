using System.ComponentModel.DataAnnotations;
using BL.Domain.Answers;

namespace BL.Domain;

public class Session
{
    [Key]
    public int Id { get; set; }
    public int FlowId { get; set; }
    public int QuestionId { get; set; }
    public string Answer { get; set; }
    public string UserId { get; set; }
    public ICollection<Answer> Answers { get; set; }
    
    public Session() { }
    
    public Session(int flowId, int questionId, string answer, string userId)
    {
        FlowId = flowId;
        QuestionId = questionId;
        Answer = answer;
        UserId = userId;
    }
}