using BL.Domain;
using BL.Domain.Questions;
using DAL.EF;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class FlowRepository(PhygitalDbContext context) : Repository(context), IFlowRepository
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
    public IEnumerable<Flow> GetFlowsByParentId(int flowId)
    {
        return _context.Set<Flow>().Where(f => f.ParentFlowId == flowId);
    }
    public Flow getFlowById(int id)
    {
        return _context.Set<Flow>().Find(id);
    }
    public IEnumerable<Question> GetQuestionsByFlowId(int id)
    {
        return _context.Set<Question>().Where(q => q.FlowId == id);
    }

    public Question GetQuestionById(int questionId)
    {
        return _context.Set<Question>().Find(questionId);
    }
} 