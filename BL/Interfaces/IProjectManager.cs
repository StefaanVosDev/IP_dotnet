using BL.Domain;

namespace BL.Interfaces;

public interface IProjectManager : IManager<Project>
{
    ValueTask<Project> FindByIdAsync(int id);
    public IEnumerable<Flow> GetParentFlowsByProjectId(int projectId);
    public Task<IEnumerable<Flow>> GetFlowsByProjectIdAsync(int projectId);
    public IEnumerable<Project> GetProjectsByAdminId(string adminId);
    public IEnumerable<Flow> GetAvailableFlowsByProjectId(int projectId);
    public IEnumerable<Project> GetProjectsByFacilitatorId(string userId);
}