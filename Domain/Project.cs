namespace BL.Domain;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Flow> Flows { get; set; }
    
    public Project(string name, List<Flow> flows, string description)
    {
        Name = name;
        Flows = flows;
        Description = description;
    }
    
    public Project(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public Project()
    {
    }
}