using BL.Domain;
using BL.Domain.Answers;
using BL.Domain.Questions;

namespace BL.Interfaces;

public interface IFlowManager : IManager<Flow>
{
    public IEnumerable<Flow> GetFlowsByProjectId(int projectId);
    public IEnumerable<Flow> GetParentFlowsByProjectId(int projectId);
    public IEnumerable<Flow> GetFlowsByParentId(int flowId);
    public Flow GetFlowById(int id);
    public Flow GetFirstSubFlowByParentId(int id);
    public IEnumerable<Question> GetQuestionsByFlowId(int id);
    public Question GetQuestionById(int questionId);
    void StoreAnswers(int id, List<Answer> answers);
}