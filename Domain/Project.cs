namespace BL.Domain;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Flow> Flows { get; set; }
    
    public Project(string name, List<Flow> flows)
    {
        Name = name;
        Flows = flows;
    }
    
    public Project(string name)
    {
        Name = name;
    }
    
    public Project()
    {
    }
}