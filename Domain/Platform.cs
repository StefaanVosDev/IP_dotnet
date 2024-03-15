using System.ComponentModel.DataAnnotations;

namespace BL.Domain
{
    /// <summary>
    /// Represents a Platform entity with a unique ID, name, description, version, and associated projects.
    /// </summary>
    public class Platform
    {
        /// <summary>
        /// Gets or sets the unique identifier for the platform.
        /// </summary>
        [Key]
        public int PlatformId { get; set; }

        /// <summary>
        /// Gets or sets the name of the platform.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the platform.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the version of the platform.
        /// </summary>
        public long Version { get; set; }

        /// <summary>
        /// Gets or sets the projects associated with the platform.
        /// </summary>
        public IEnumerable<Project> Projects { get; set; }

        /// <summary>
        /// Gets or sets the platform administrator associated with the platform.
        /// </summary>
        public PlatformAdministrator PlatformAdministrator { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the platform administrator associated with the platform.
        /// </summary>
        public int PlatformAdministratorId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Platform"/> class.
        /// </summary>
        public Platform()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Platform"/> class with the specified name, description, projects, and platform administrator.
        /// </summary>
        /// <param name="name">The name of the platform.</param>
        /// <param name="description">The description of the platform.</param>
        /// <param name="projects">The projects associated with the platform.</param>
        /// <param name="platformAdministrator">The platform administrator associated with the platform.</param>
        public Platform(string name, string description, IEnumerable<Project> projects, PlatformAdministrator platformAdministrator)
        {
            Name = name;
            Description = description;
            Projects = projects;
            PlatformAdministrator = platformAdministrator;
        }
    }
}