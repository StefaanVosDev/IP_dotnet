using BL.Domain;
using BL.Domain.Answers;
using BL.Domain.Questions;

namespace BL.Interfaces;

public interface IFlowManager : IManager<Flow>
{
    public IEnumerable<Flow> GetFlowsByParentId(int flowId);
    public Flow GetFlowById(int id);
    public Flow GetFirstSubFlowByParentId(int id);
    public IEnumerable<Question> GetQuestionsByFlowId(int id);
    public Queue<int> GetQuestionQueueByFlowId(int flowId);
    public int? GetParentFlowIdBySessionId(int sessionId);
    public bool IsParentFlow(int flowId);
}