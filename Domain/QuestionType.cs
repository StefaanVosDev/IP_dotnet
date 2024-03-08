namespace BL.Domain
{
    /// <summary>
    /// Represents different types of questions that can be asked in a survey.
    /// </summary>
    public enum QuestionType
    {
        /// <summary>
        /// Represents a question where multiple options can be selected.
        /// </summary>
        MULTIPLE_CHOICE,

        /// <summary>
        /// Represents a question where the respondent can write an open-ended response.
        /// </summary>
        OPEN,

        /// <summary>
        /// Represents a question where the respondent selects a value from a range.
        /// </summary>
        RANGE,

        /// <summary>
        /// Represents a question where only one option can be selected.
        /// </summary>
        SINGLE_CHOICE
    }
}