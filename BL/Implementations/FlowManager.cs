using BL.Domain;
using BL.Domain.Answers;
using BL.Domain.Questions;
using BL.Interfaces;
using DAL.Interfaces;

namespace BL.Implementations;

public class FlowManager(IFlowRepository repository) : Manager<Flow>(repository), IFlowManager
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

    public IEnumerable<Question> GetQuestionsByFlowId(int id)
    {
        return repository.GetQuestionsByFlowId(id);
    }
}