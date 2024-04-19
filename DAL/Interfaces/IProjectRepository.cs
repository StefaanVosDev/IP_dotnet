using BL.Domain;
using BL.Domain.Answers;

namespace DAL.Interfaces;

public interface IProjectRepository : IRepository
{
    public IEnumerable<Flow> GetFlowsByProjectIdAsync(int projectId);
    public IEnumerable<Flow> GetParentFlowsByProjectId(int projectId);     
    public void StoreAnswers(int id, List<Answer> answers);
    public IEnumerable<Project> GetProjectsByAdminId(string adminId);
}