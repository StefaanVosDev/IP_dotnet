namespace BL.Domain;

public class Project(string name, List<Flow> flows)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public List<Flow> Flows { get; set; } = flows;
}