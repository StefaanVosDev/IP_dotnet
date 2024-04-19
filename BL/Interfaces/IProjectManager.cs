using BL.Domain;
using BL.Domain.Answers;

namespace BL.Interfaces;

public interface IProjectManager : IManager<Project>
{
    ValueTask<Project> FindByIdAsync(int id);
    public IEnumerable<Flow> GetParentFlowsByProjectId(int projectId);
    public IEnumerable<Flow> GetFlowsByProjectId(int projectId);
    public void StoreAnswers(int id, List<Answer> answers);
    public IEnumerable<Project> GetProjectsByAdminId(string adminId);
}