using BL.Domain;
using BL.Domain.Questions;

namespace DAL.Interfaces;

public interface IFlowRepository : IRepository
{
    public IEnumerable<Flow> GetFlowsByProjectIdAsync(int projectId);
    public IEnumerable<Flow> GetParentFlowsByProjectId(int projectId);
    public IEnumerable<Flow> GetFlowsByParentId(int flowId);
    public Flow getFlowById(int id);
    public IEnumerable<Question> GetQuestionsByFlowId(int id);
}