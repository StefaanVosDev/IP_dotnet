using BL.Domain;

namespace BL.Interfaces;

public interface IProjectManager : IManager<Project>
{
    ValueTask<Project> FindByIdAsync(int id);
    Task<IEnumerable<Flow>> GetParentFlowsByProjectIdAsync(int projectId);
    Task<IEnumerable<Flow>> GetFlowsByProjectIdAsync(int projectId);
    Task<IEnumerable<Project>> GetProjectsByAdminIdAsync(string adminId);
    Task<IEnumerable<Flow>> GetAvailableFlowsByProjectIdAsync(int projectId);
}