using BL.Domain;
using DAL.Interfaces;

namespace BL.Implementations
{
    public class FlowManager(IFlowRepository repository) : Manager<Flow>(repository), IFlowManager
    {
        public IEnumerable<Flow> ReadFlowsByProjectId(int projectId)
        {
            return repository.GetFlowsByProjectIdAsync(projectId).Result;
        }
    }
}