using BL.Domain;

namespace DAL.Interfaces;

public interface IFlowRepository : IRepository
{
    Task<IEnumerable<Flow>> GetFlowsByProjectIdAsync(int projectId);
}