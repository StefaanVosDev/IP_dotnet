using System.ComponentModel.DataAnnotations;

namespace BL.Domain
{
    /// <summary>
    /// Represents a Platform Administrator entity with a unique ID, name, email, password, associated platforms, and project administrator.
    /// </summary>
    public class PlatformAdministrator
    {
        /// <summary>
        /// Gets or sets the unique identifier for the platform administrator.
        /// </summary>
        [Key]
        public int PlatformAdministratorId { get; set; }

        /// <summary>
        /// Gets or sets the name of the platform administrator.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the platform administrator.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the platform administrator.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the project administrator associated with the platform administrator.
        /// </summary>
        public ProjectAdministrator ProjectAdministrator { get; set; }

        /// <summary>
        /// Gets or sets the platforms associated with the platform administrator.
        /// </summary>
        public IEnumerable<Platform> Platforms { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformAdministrator"/> class.
        /// </summary>
        public PlatformAdministrator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformAdministrator"/> class with the specified name, email, password, platforms, and project administrator.
        /// </summary>
        /// <param name="name">The name of the platform administrator.</param>
        /// <param name="email">The email of the platform administrator.</param>
        /// <param name="password">The password of the platform administrator.</param>
        /// <param name="platforms">The platforms associated with the platform administrator.</param>
        /// <param name="projectAdministrator">The project administrator associated with the platform administrator.</param>
        public PlatformAdministrator(string name, string email, string password, IEnumerable<Platform> platforms, ProjectAdministrator projectAdministrator)
        {
            Name = name;
            Email = email;
            Password = password;
            Platforms = platforms;
            ProjectAdministrator = projectAdministrator;
        }
    }
}