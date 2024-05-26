using BL.Domain;
using DAL.EF;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class ProjectRepository : Repository, IProjectRepository
{
    private readonly PhygitalDbContext _context;

    public ProjectRepository(PhygitalDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Flow>> GetFlowsByProjectIdAsync(int projectId)
    {
        return await _context.Set<Flow>().Where(f => f.ProjectId == projectId).ToListAsync();
    }

    public async Task<IEnumerable<Flow>> GetParentFlowsByProjectIdAsync(int projectId)
    {
        return await _context.Set<Flow>().Where(f => f.ProjectId == projectId && f.ParentFlowId == null).ToListAsync();
    }

    public async Task<IEnumerable<Project>> GetProjectsByAdminIdAsync(string adminId)
    {
        return await _context.Set<Project>().Where(p => p.AdminId == adminId).ToListAsync();
    }

    public async ValueTask<Project> FindByIdAsync(int id)
    {
        return await _context.Set<Project>().FindAsync(id);
    }

    public async Task<IEnumerable<Flow>> FindAvailableFlowsByProjectIdAsync(int projectId, DateTime date)
    {
        return await _context.Set<Flow>().Where(f => f.ProjectId == projectId &&
                                                     f.ParentFlowId == null &&
                                                     f.StartDate <= date &&
                                                     f.EndDate >= date).ToListAsync();
    }
}
