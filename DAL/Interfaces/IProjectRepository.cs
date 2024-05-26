using BL.Domain;
using BL.Domain.Answers;

namespace DAL.Interfaces;

public interface IProjectRepository : IRepository
{
    Task<IEnumerable<Flow>> GetFlowsByProjectIdAsync(int projectId);
    Task<IEnumerable<Flow>> GetParentFlowsByProjectIdAsync(int projectId);
    Task<IEnumerable<Project>> GetProjectsByAdminIdAsync(string adminId);
    ValueTask<Project> FindByIdAsync(int id);
    Task<IEnumerable<Flow>> FindAvailableFlowsByProjectIdAsync(int projectId, DateTime date);
}
