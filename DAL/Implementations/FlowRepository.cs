using BL.Domain;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class FlowRepository(DbContext context) : Repository(context), IFlowRepository
{
    private readonly DbContext _context = context;

    public Task<IEnumerable<Flow>> GetFlowsByProjectIdAsync(int projectId)
    {
        return Task.FromResult(_context.Set<Flow>().Where(f => f.ProjectId == projectId).AsEnumerable());
    }
} 