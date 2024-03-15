using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BL.Domain
{
    /// <summary>
    /// Represents a Session entity with a unique ID, start time, end time, location, associated flows, respondents, and facilitator.
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Gets or sets the unique identifier for the session.
        /// </summary>
        [Key]
        public int SessionId { get; set; }

        /// <summary>
        /// Gets or sets the start time of the session.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of the session.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the location of the session.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the flows associated with the session.
        /// </summary>
        public IEnumerable<Flow> Flows { get; set; }

        /// <summary>
        /// Gets or sets the respondents of the session.
        /// </summary>
        public IEnumerable<Respondent> Respondents { get; set; }

        /// <summary>
        /// Gets or sets the facilitator associated with the session.
        /// </summary>
        public Facilitator Facilitator { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the facilitator associated with the session.
        /// </summary>
        public int FacilitatorId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        public Session()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class with the specified start time, end time, location, flows, respondents, and facilitator.
        /// </summary>
        /// <param name="startTime">The start time of the session.</param>
        /// <param name="endTime">The end time of the session.</param>
        /// <param name="location">The location of the session.</param>
        /// <param name="flows">The flows associated with the session.</param>
        /// <param name="respondents">The respondents of the session.</param>
        /// <param name="facilitator">The facilitator associated with the session.</param>
        public Session(DateTime startTime, DateTime endTime, string location, IEnumerable<Flow> flows, IEnumerable<Respondent> respondents, Facilitator facilitator)
        {
            StartTime = startTime;
            EndTime = endTime;
            Location = location;
            Flows = flows;
            Respondents = respondents;
            Facilitator = facilitator;
        }
    }
}