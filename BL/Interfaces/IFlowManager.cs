using System.Collections;
using BL.Domain;

namespace BL;

public interface IFlowManager : IManager<Flow>
{
    public IEnumerable<Flow> ReadFlowsByProjectId(int projectId);
}