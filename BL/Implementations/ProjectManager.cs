using BL.Domain;
using BL.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BL.Implementations;

public class ProjectManager : Manager<Project>, IProjectManager
{
    private readonly IProjectRepository _repository;

    public ProjectManager(IProjectRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Flow>> GetFlowsByProjectIdAsync(int projectId)
    {
        return await _repository.GetFlowsByProjectIdAsync(projectId);
    }

    public async ValueTask<Project> FindByIdAsync(int id)
    {
        return await _repository.FindByIdAsync(id);
    }

    public async Task<IEnumerable<Flow>> GetParentFlowsByProjectIdAsync(int projectId)
    {
        return await _repository.GetParentFlowsByProjectIdAsync(projectId);
    }

    public async Task<IEnumerable<Project>> GetProjectsByAdminIdAsync(string adminId)
    {
        return await _repository.GetProjectsByAdminIdAsync(adminId);
    }

    public async Task<IEnumerable<Flow>> GetAvailableFlowsByProjectIdAsync(int projectId)
    {
        DateTime today = DateTime.Today.ToUniversalTime();
        return await _repository.FindAvailableFlowsByProjectIdAsync(projectId, today);
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