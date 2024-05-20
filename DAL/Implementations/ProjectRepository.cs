using BL.Domain;
using BL.Domain.Answers;
using DAL.EF;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class ProjectRepository(PhygitalDbContext context) : Repository(context), IProjectRepository
{
    private readonly DbContext _context = context;

    public async Task<IEnumerable<Flow>> GetFlowsByProjectIdAsync(int projectId)
    {
        return await _context.Set<Flow>().Where(f => f.ProjectId == projectId).ToListAsync();
    }

    public IEnumerable<Flow> GetParentFlowsByProjectId(int projectId)
    {
        return _context.Set<Flow>().Where(f => f.ProjectId == projectId && f.ParentFlowId == null);
    }

    public void StoreAnswers(int id, List<Answer> answers)
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<Project> GetProjectsByAdminId(string adminId)
    {
        return _context.Set<Project>().Where(p => p.AdminId == adminId);
    }

    public ValueTask<Project> FindByIdAsync(int id)
    {
        return _context.Set<Project>().FindAsync(id);
    }
}