using BL.Domain;
using BL.Domain.Answers;
using DAL.EF;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class ProjectRepository(PhygitalDbContext context) : Repository(context), IProjectRepository
{
    private readonly DbContext _context = context;

    public IEnumerable<Flow> GetFlowsByProjectIdAsync(int projectId)
    {
        return _context.Set<Flow>().Where(f => f.ProjectId == projectId);
    }

    public IEnumerable<Flow> GetParentFlowsByProjectId(int projectId)
    {
        return _context.Set<Flow>().Where(f => f.ProjectId == projectId && f.ParentFlowId == null);
    }

    public void StoreAnswers(int id, List<Answer> answers)
    {
        throw new NotImplementedException();
    }
}