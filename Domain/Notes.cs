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
        public int NoteId { get; set; }

        /// <summary>
        /// Gets or sets the text of the note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notes"/> class.
        /// </summary>
        public Notes()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notes"/> class with the specified note text.
        /// </summary>
        /// <param name="note">The text of the note.</param>
        public Notes(string note)
        {
            this.Note = note;
        }
    }
}