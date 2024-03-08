namespace BL.Domain
{
    /// <summary>
    /// Represents a Project Administrator entity.
    /// </summary>
    public class ProjectAdministrator
    {
        /// <summary>
        /// Gets or sets the unique identifier for the project administrator.
        /// </summary>
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
        /// Initializes a new instance of the <see cref="ProjectAdministrator"/> class.
        /// </summary>
        public ProjectAdministrator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectAdministrator"/> class with the specified name, email, password, and organisation.
        /// </summary>
        /// <param name="name">The name of the project administrator.</param>
        /// <param name="email">The email of the project administrator.</param>
        /// <param name="password">The password of the project administrator.</param>
        /// <param name="organisation">The organisation that the project administrator belongs to.</param>
        public ProjectAdministrator(string name, string email, string password, string organisation)
        {
            Name = name;
            Email = email;
            Password = password;
            Organisation = organisation;
        }
    }
}