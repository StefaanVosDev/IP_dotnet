using System.ComponentModel.DataAnnotations;

namespace BL.Domain;

public class DataAnalysis
{
    //TODO: finish DataAnalysis class
    [Key]
    public int AnalysisId { get; set; }
    public Project Project { get; set; }
    public int ProjectId { get; set; }
    public ProjectAdministrator ProjectAdministrator { get; set; }
    public int ProjectAdministratorId { get; set; }
    
    public DataAnalysis()
    {
    }
    
    public DataAnalysis(int analysisId, Project project, ProjectAdministrator projectAdministrator)
    {
        this.AnalysisId = analysisId;
        Project = project;
        ProjectAdministrator = projectAdministrator;
    }
}