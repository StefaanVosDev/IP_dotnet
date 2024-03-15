using System.ComponentModel.DataAnnotations;

namespace BL.Domain
{
    /// <summary>
    /// Represents a Respondent entity with a unique ID, name, email, associated feedback, facilitator, installations, and session.
    /// </summary>
    public class Respondent
    {
        /// <summary>
        /// Gets or sets the unique identifier for the respondent.
        /// </summary>
        [Key]
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
        /// Gets or sets the facilitator associated with the respondent.
        /// </summary>
        public Facilitator Facilitator { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the facilitator associated with the respondent.
        /// </summary>
        public int FacilitatorId { get; set; }

        /// <summary>
        /// Gets or sets the installations associated with the respondent.
        /// </summary>
        public IEnumerable<Installation> Installations { get; set; }

        /// <summary>
        /// Gets or sets the session associated with the respondent.
        /// </summary>
        public Session Session { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the session associated with the respondent.
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Respondent"/> class.
        /// </summary>
        public Respondent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Respondent"/> class with the specified name, email, feedback, session, facilitator, and installations.
        /// </summary>
        /// <param name="name">The name of the respondent.</param>
        /// <param name="email">The email of the respondent.</param>
        /// <param name="feedback">The feedback associated with the respondent.</param>
        /// <param name="session">The session associated with the respondent.</param>
        /// <param name="facilitator">The facilitator associated with the respondent.</param>
        /// <param name="installations">The installations associated with the respondent.</param>
        public Respondent(string name, string email, IEnumerable<Feedback> feedback, Session session, Facilitator facilitator, IEnumerable<Installation> installations)
        {
            Name = name;
            Email = email;
            Feedback = feedback;
            Session = session;
            Facilitator = facilitator;
            Installations = installations;
        }
    }
}