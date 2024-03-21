namespace BL.Domain;

public class Session
{
    public int FlowId { get; set; }
    public int QuestionId { get; set; }
    public string Answer { get; set; }
    public string UserId { get; set; }
    
    public Session() { }
    
    public Session(int flowId, int questionId, string answer, string userId)
    {
        FlowId = flowId;
        QuestionId = questionId;
        Answer = answer;
        UserId = userId;
    }
}