using BL.Domain;
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
    
    public IEnumerable<Project> GetProjectsByAdminId(string adminId)
    {
        return _context.Set<Project>().Where(p => p.AdminId == adminId);
    }

    public ValueTask<Project> FindByIdAsync(int id)
    {
        return _context.Set<Project>().FindAsync(id);
    }

    public IEnumerable<Flow> FindAvailableFlowsByProjectId(int projectId, DateTime date)
    {
        return _context.Set<Flow>().Where(f => f.ProjectId == projectId &&
                                               f.ParentFlowId == null && 
                                               f.StartDate <= date &&
                                               f.EndDate >= date
                                               );
    }

    public IEnumerable<Project> GetProjectsByFacilitatorId(string userId)
    {
        return _context.Set<ProjectFacilitator>()
            .Where(pf => pf.FacilitatorId == userId)
            .Select(pf => pf.Project)
            .ToList();
    }
}