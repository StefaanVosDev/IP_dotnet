namespace BL.Domain;

/// <summary>
/// Represents a Project with a unique ID, title, description, and start and end dates.
/// </summary>
public class Project
{
    /// <summary>
    /// Gets or sets the unique identifier for the project.
    /// </summary>
    public int ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the title of the project.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the project.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the start date of the project.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the project.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the flows of the project.
    /// </summary>
    public IEnumerable<Flow> Flows { get; set; }
    
    /// <summary>
    /// Gets or sets the data analysis of the project.
    /// </summary>
    public DataAnalysis DataAnalysis { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Project"/> class.
    /// </summary>
    public Project(DataAnalysis dataAnalysis)
    {
        DataAnalysis = dataAnalysis;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Project"/> class with the specified title, description, start date, end date, and flows.
    /// </summary>
    /// <param name="title">The title of the project.</param>
    /// <param name="description">The description of the project.</param>
    /// <param name="startDate">The start date of the project.</param>
    /// <param name="endDate">The end date of the project.</param>
    /// <param name="flows">The flows of the project.</param>
    /// <param name="dataAnalysis"></param>
    public Project(string title, string description, DateTime startDate, DateTime endDate, IEnumerable<Flow> flows, DataAnalysis dataAnalysis)
    {
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Flows = flows;
        DataAnalysis = dataAnalysis;
    }
}