using System.ComponentModel.DataAnnotations;

namespace BL.Domain
{
    /// <summary>
    /// Represents a Notes entity with a unique ID and note text.
    /// </summary>
    public class Notes
    {
        /// <summary>
        /// Gets or sets the unique identifier for the note.
        /// </summary>
        [Key]
        public int NoteId { get; set; }

        /// <summary>
        /// Gets or sets the text of the note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the Facilitator associated with the note.
        /// </summary>
        public Facilitator Facilitator { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the Facilitator associated with the note.
        /// </summary>
        public int FacilitatorId { get; set; }

        /// <summary>
        /// Gets or sets the Question associated with the note.
        /// </summary>
        public Question Question { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notes"/> class.
        /// </summary>
        public Notes()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notes"/> class with the specified note text, Facilitator, and Question.
        /// </summary>
        /// <param name="note">The text of the note.</param>
        /// <param name="facilitator">The Facilitator associated with the note.</param>
        /// <param name="question">The Question associated with the note.</param>
        public Notes(string note, Facilitator facilitator, Question question)
        {
            this.Note = note;
            Facilitator = facilitator;
            Question = question;
        }
    }
}