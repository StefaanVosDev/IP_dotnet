using BL.Domain;
using BL.Domain.Answers;

namespace BL.Interfaces;

public interface IProjectManager : IManager<Project>
{
    public IEnumerable<Flow> GetParentFlowsByProjectId(int projectId);
    public IEnumerable<Flow> GetFlowsByProjectId(int projectId);
    void StoreAnswers(int id, List<Answer> answers);
}