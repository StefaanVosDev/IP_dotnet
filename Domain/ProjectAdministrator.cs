using System.ComponentModel.DataAnnotations;

namespace BL.Domain
{
    /// <summary>
    /// Represents a Project Administrator entity with a unique ID, name, email, password, organisation, associated platform administrator, data analyses, and projects.
    /// </summary>
    public class ProjectAdministrator
    {
        /// <summary>
        /// Gets or sets the unique identifier for the project administrator.
        /// </summary>
        [Key]
        public int ProjectAdministratorId { get; set; }

        /// <summary>
        /// Gets or sets the name of the project administrator.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the project administrator.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the project administrator.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the organisation that the project administrator belongs to.
        /// </summary>
        public string Organisation { get; set; }

        /// <summary>
        /// Gets or sets the platform administrator associated with the project administrator.
        /// </summary>
        public PlatformAdministrator PlatformAdministrator { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the platform administrator associated with the project administrator.
        /// </summary>
        public int PlatformAdministratorId { get; set; }

        /// <summary>
        /// Gets or sets the data analyses associated with the project administrator.
        /// </summary>
        public IEnumerable<DataAnalysis> DataAnalyses { get; set; }

        /// <summary>
        /// Gets or sets the projects associated with the project administrator.
        /// </summary>
        public IEnumerable<Project> Projects { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectAdministrator"/> class.
        /// </summary>
        public ProjectAdministrator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectAdministrator"/> class with the specified name, email, password, organisation, platform administrator, data analyses, and projects.
        /// </summary>
        /// <param name="name">The name of the project administrator.</param>
        /// <param name="email">The email of the project administrator.</param>
        /// <param name="password">The password of the project administrator.</param>
        /// <param name="organisation">The organisation that the project administrator belongs to.</param>
        /// <param name="platformAdministrator">The platform administrator associated with the project administrator.</param>
        /// <param name="dataAnalyses">The data analyses associated with the project administrator.</param>
        /// <param name="projects">The projects associated with the project administrator.</param>
        public ProjectAdministrator(string name, string email, string password, string organisation, PlatformAdministrator platformAdministrator, IEnumerable<DataAnalysis> dataAnalyses, IEnumerable<Project> projects)
        {
            Name = name;
            Email = email;
            Password = password;
            Organisation = organisation;
            PlatformAdministrator = platformAdministrator;
            DataAnalyses = dataAnalyses;
            Projects = projects;
        }
    }
}