using BL.Domain;
using BL.Domain.Questions;
using Microsoft.AspNetCore.Identity;

namespace BL.Domain;

public class Flow
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public FlowType Type { get; set; }
    public List<Question> Questions { get; set; }
    public int? ParentFlowId { get; set; }
    public Flow ParentFlow { get; set; }
    public List<Flow> SubFlows { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    public Media Media { get; set; }
    
    public Flow()
    {
    }

    public Flow(string name, string description, int projectId, FlowType type, List<Question> questions, List<Flow> subFlows, Media media)
    {
        Name = name;
        Description = description;
        ProjectId = projectId;
        Type = type;
        Questions = questions;
        SubFlows = subFlows;
        Media = media;
    }
}
