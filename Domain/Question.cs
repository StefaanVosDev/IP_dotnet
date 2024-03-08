using System.Collections;

namespace BL.Domain
{
    /// <summary>
    /// Represents a Question entity with a unique ID, type, question text, sub-questions, and notes.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Gets or sets the unique identifier for the question.
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Gets or sets the type of the question.
        /// </summary>
        public QuestionType Type { get; set; }

        /// <summary>
        /// Gets or sets the text of the question.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets the sub-questions of the question.
        /// </summary>
        public IEnumerable<Question> SubQuestions { get; set; }

        /// <summary>
        /// Gets or sets the notes of the question.
        /// </summary>
        public IEnumerable<Notes> Notes { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Question"/> class.
        /// </summary>
        public Question()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Question"/> class with the specified type and question text.
        /// </summary>
        /// <param name="type">The type of the question.</param>
        /// <param name="question">The text of the question.</param>
        /// <param name="subQuestions"></param>
        /// <param name="notes"></param>
        public Question(QuestionType type, string question, IEnumerable<Question> subQuestions ,IEnumerable<Notes> notes)
        {
            Type = type;
            QuestionText = question;
            SubQuestions = subQuestions;
            Notes = notes;
        }
    }
}