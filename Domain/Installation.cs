namespace BL.Domain;

public class Installation
{
    //TODO: finish Installation class
    
    public int InstallationId { get; set; }
    public long Version { get; set; }
    public Facilitator Facilitator { get; set; }
    public int FacilitatorId { get; set; }
    public IEnumerable<Flow> Flows { get; set; }
    public Respondent Respondent { get; set; }
    public int RespondentId { get; set; }

    public Installation()
    {
    }
    
    public Installation(long version)
    {
        this.Version = version;
    }
}