using BL.Domain;
using BL.Interfaces;
using DAL.Interfaces;

namespace BL.Implementations;

public class ProjectManager(IProjectRepository repository) : Manager<Project>(repository), IProjectManager
{
    public async Task<IEnumerable<Flow>> GetFlowsByProjectIdAsync(int projectId)
    {
        return await repository.GetFlowsByProjectIdAsync(projectId);
    }

    public ValueTask<Project> FindByIdAsync(int id)
    {
        return repository.FindByIdAsync(id);
    }

    public IEnumerable<Flow> GetParentFlowsByProjectId(int projectId)
    {
        return repository.GetParentFlowsByProjectId(projectId);
    }

    public IEnumerable<Project> GetProjectsByAdminId(string adminId)
    {
        return repository.GetProjectsByAdminId(adminId);
    }

    public IEnumerable<Flow> GetAvailableFlowsByProjectId(int projectId)
    {
        DateTime today = DateTime.Today.ToUniversalTime();
        return repository.FindAvailableFlowsByProjectId(projectId, today);
    }

    public IEnumerable<Project> GetProjectsByFacilitatorId(string userId)
    {
        return repository.GetProjectsByFacilitatorId(userId);
    }
}