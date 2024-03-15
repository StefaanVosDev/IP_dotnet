namespace BL.Domain
{
    /// <summary>
    /// Represents a Feedback entity with a unique ID, timestamp, message, and associated respondent.
    /// This is an abstract class and cannot be instantiated directly.
    /// </summary>
    public class Feedback
    {
        /// <summary>
        /// Gets or sets the unique identifier for the feedback.
        /// </summary>
        public int FeedbackId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the feedback.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the message of the feedback.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the respondent associated with the feedback.
        /// </summary>
        public Respondent Respondent { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the respondent associated with the feedback.
        /// </summary>
        public int RespondentId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Feedback"/> class.
        /// </summary>
        public Feedback()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Feedback"/> class with the specified timestamp, message, and respondent.
        /// </summary>
        /// <param name="timestamp">The timestamp of the feedback.</param>
        /// <param name="message">The message of the feedback.</param>
        /// <param name="respondent">The respondent associated with the feedback.</param>
        public Feedback(DateTime timestamp, string message, Respondent respondent)
        {
            Timestamp = timestamp;
            Message = message;
            Respondent = respondent;
        }
    }
}