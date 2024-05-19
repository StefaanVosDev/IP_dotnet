using BL.Domain;
using BL.Domain.Answers;

namespace BL.Interfaces;

public interface IProjectManager : IManager<Project>
{
    ValueTask<Project> FindByIdAsync(int id);
    public IEnumerable<Flow> GetParentFlowsByProjectId(int projectId);
    public Project GetProjectByProjectId(int projectId);
    public Task<IEnumerable<Flow>> GetFlowsByProjectIdAsync(int projectId);
    public void StoreAnswers(int id, List<Answer> answers);
    public void ChangeProject(int projectId, string name, string description);
    public IEnumerable<Project> GetProjectsByAdminId(string adminId);
}