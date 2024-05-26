using BL.Domain;
using BL.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

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

    public IEnumerable<IdentityUser> GetSearchedFacilitators(string searchTerm)
    {
        return repository.GetSearchedFacilitators(searchTerm);
    }

    public IEnumerable<IdentityUser> GetFacilitatorsByProjectId(int projectId)
    {
        return repository.GetFacilitatorsByProjectId(projectId);
    }

    public bool AddFacilitatorToProject(string userId, int projectId)
    {
        return repository.AddFacilitatorToProject(userId, projectId);
    }

    public bool RemoveFacilitatorFromProject(string userId, int projectId)
    {
        return repository.RemoveFacilitatorFromProject(userId, projectId);
    }
}