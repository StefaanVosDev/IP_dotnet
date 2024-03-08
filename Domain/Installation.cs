namespace BL.Domain;

public class Installation
{
    //TODO: finish Installation class
    
    public int InstallationId { get; set; }
    public long Version { get; set; }
    
    public Installation()
    {
    }
    
    public Installation(long version)
    {
        this.Version = version;
    }
}