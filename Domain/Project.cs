using System.ComponentModel.DataAnnotations;

namespace BL.Domain
{
    /// <summary>
    /// Represents a Project entity with a unique ID, title, description, start and end dates, flows, data analysis, project administrator, and platforms.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the unique identifier for the project.
        /// </summary>
        [Key]
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
        /// Gets or sets the flows associated with the project.
        /// </summary>
        public IEnumerable<Flow> Flows { get; set; }

        /// <summary>
        /// Gets or sets the data analysis associated with the project.
        /// </summary>
        public DataAnalysis DataAnalysis { get; set; }

        /// <summary>
        /// Gets or sets the data analyses associated with the project.
        /// </summary>
        public IEnumerable<DataAnalysis> DataAnalyses { get; set; }

        /// <summary>
        /// Gets or sets the project administrator associated with the project.
        /// </summary>
        public ProjectAdministrator ProjectAdministrator { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the project administrator associated with the project.
        /// </summary>
        public int ProjectAdministratorId { get; set; }

        /// <summary>
        /// Gets or sets the platforms associated with the project.
        /// </summary>
        public IEnumerable<Platform> Platforms { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        public Project()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class with the specified title, description, start date, end date, flows, data analysis, project administrator, and platforms.
        /// </summary>
        /// <param name="title">The title of the project.</param>
        /// <param name="description">The description of the project.</param>
        /// <param name="startDate">The start date of the project.</param>
        /// <param name="endDate">The end date of the project.</param>
        /// <param name="flows">The flows associated with the project.</param>
        /// <param name="dataAnalysis">The data analysis associated with the project.</param>
        /// <param name="projectAdministrator">The project administrator associated with the project.</param>
        /// <param name="platforms">The platforms associated with the project.</param>
        public Project(string title, string description, DateTime startDate, DateTime endDate, IEnumerable<Flow> flows, DataAnalysis dataAnalysis, ProjectAdministrator projectAdministrator, IEnumerable<Platform> platforms)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Flows = flows;
            DataAnalysis = dataAnalysis;
            ProjectAdministrator = projectAdministrator;
            Platforms = platforms;
        }
    }
}