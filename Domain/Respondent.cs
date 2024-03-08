namespace BL.Domain
{
    /// <summary>
    /// Represents a Respondent entity with a unique ID, name, email, and associated feedback.
    /// </summary>
    public class Respondent
    {
        /// <summary>
        /// Gets or sets the unique identifier for the respondent.
        /// </summary>
        public int RespondentId { get; set; }

        /// <summary>
        /// Gets or sets the name of the respondent.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the respondent.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the feedback associated with the respondent.
        /// </summary>
        public IEnumerable<Feedback> Feedback { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Respondent"/> class.
        /// </summary>
        public Respondent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Respondent"/> class with the specified name, email, and feedback.
        /// </summary>
        /// <param name="name">The name of the respondent.</param>
        /// <param name="email">The email of the respondent.</param>
        /// <param name="feedback">The feedback associated with the respondent.</param>
        public Respondent(string name, string email, IEnumerable<Feedback> feedback)
        {
            Name = name;
            Email = email;
            Feedback = feedback;
        }
    }
}