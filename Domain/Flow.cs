using BL.Domain;
using BL.Domain.Questions;

namespace BL.Domain;

public class Flow
{
    public int Id { get; set; }
    public string Name { get; set; }
    public FlowType Type { get; set; }
    public List<Question> Questions { get; set; }
    public int? ParentFlowId { get; set; }
    public Flow ParentFlow { get; set; }
    public List<Flow> SubFlows { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    
    public Flow(FlowType type)
    {
        Type = type;
    }

    public Flow(string name, FlowType type, List<Question> questions, List<Flow> subFlows)
    {
        Questions = questions;
        SubFlows = subFlows;
        Type = type;
        Name = name;
    }
}
