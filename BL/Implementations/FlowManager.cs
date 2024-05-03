using BL.Domain;
using BL.Domain.Answers;
using BL.Domain.Questions;
using BL.Interfaces;
using DAL.Interfaces;


namespace BL.Implementations;

public class FlowManager(IFlowRepository repository, ISessionManager sessionManager) : Manager<Flow>(repository), IFlowManager
{
    public IEnumerable<Flow> GetFlowsByParentId(int flowId)
    {
        return repository.GetFlowsByParentId(flowId);
    }

    public Flow GetFlowById(int id)
    {
        return repository.getFlowById(id);
    }

    public Flow GetFirstSubFlowByParentId(int id)
    {
        return repository.GetFlowsByParentId(id).FirstOrDefault();
    }

    public async Task<IEnumerable<Question>> GetQuestionsByFlowIdAsync(int flowId)
    {
        return repository.GetQuestionsByFlowId(flowId);
    }
    
    public async Task<Queue<int>> GetQuestionQueueByFlowIdAsync(int flowId)
    {
        var questionIds = (await GetQuestionsByFlowIdAsync(flowId)).Select(q => q.Id).ToList();
        return new Queue<int>(questionIds);
    }
    
    public int? GetParentFlowIdBySessionId(int sessionId)
    {
        var flow = GetFlowById(sessionManager.GetSessionById(sessionId).FlowId);
        return flow.ParentFlowId;
    }
    
    public bool IsParentFlow(int flowId) { return GetFlowById(flowId).ParentFlowId == null; }
    public IEnumerable<Flow> GetFlowsBetweenPositions(int newPosition, int oldPosition)
    {
        return repository.GetFlowsBetweenPositions(newPosition, oldPosition);
    }
}