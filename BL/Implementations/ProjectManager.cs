using BL.Domain;
using BL.Domain.Answers;
using BL.Interfaces;
using DAL.Interfaces;

namespace BL.Implementations;

public class ProjectManager(IProjectRepository repository) : Manager<Project>(repository), IProjectManager
{
    public IEnumerable<Flow> GetFlowsByProjectId(int projectId)
    {
        return repository.GetFlowsByProjectIdAsync(projectId);
    }

    public ValueTask<Project> FindByIdAsync(int id)
    {
        return repository.FindByIdAsync(id);
    }

    public IEnumerable<Flow> GetParentFlowsByProjectId(int projectId)
    {
        return repository.GetParentFlowsByProjectId(projectId);
    }

    public void StoreAnswers(int id, List<Answer> answers)
    {
        repository.StoreAnswers(id, answers);
    }

    public IEnumerable<Project> GetProjectsByAdminId(string adminId)
    {
        return repository.GetProjectsByAdminId(adminId);
    }
}