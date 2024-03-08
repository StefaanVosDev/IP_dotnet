namespace BL.Domain
{
    /// <summary>
    /// Represents a Facilitator entity with a unique ID, name, email, phone number, associated notes, and sessions.
    /// </summary>
    public class Facilitator
    {
        /// <summary>
        /// Gets or sets the unique identifier for the facilitator.
        /// </summary>
        public int FacilitatorId { get; set; }

        /// <summary>
        /// Gets or sets the name of the facilitator.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the facilitator.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the facilitator.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the notes associated with the facilitator.
        /// </summary>
        public IEnumerable<Notes> Notes { get; set; }

        /// <summary>
        /// Gets or sets the sessions associated with the facilitator.
        /// </summary>
        public IEnumerable<Session> Sessions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Facilitator"/> class with the specified notes and sessions.
        /// </summary>
        /// <param name="notes">The notes associated with the facilitator.</param>
        /// <param name="sessions">The sessions associated with the facilitator.</param>
        public Facilitator(IEnumerable<Notes> notes, IEnumerable<Session> sessions)
        {
            Notes = notes;
            Sessions = sessions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Facilitator"/> class with the specified name, email, phone number, notes, and sessions.
        /// </summary>
        /// <param name="name">The name of the facilitator.</param>
        /// <param name="email">The email of the facilitator.</param>
        /// <param name="phoneNumber">The phone number of the facilitator.</param>
        /// <param name="notes">The notes associated with the facilitator.</param>
        /// <param name="sessions">The sessions associated with the facilitator.</param>
        public Facilitator(string name, string email, string phoneNumber, IEnumerable<Notes> notes, IEnumerable<Session> sessions)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Notes = notes;
            Sessions = sessions;
        }
    }
}