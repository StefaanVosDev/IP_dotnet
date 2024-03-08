namespace BL.Domain
{
    /// <summary>
    /// Represents different types of content that can be presented in a survey.
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// Represents a content type where the content is purely textual.
        /// </summary>
        TEXT,

        /// <summary>
        /// Represents a content type where the content is an image.
        /// </summary>
        IMAGE,

        /// <summary>
        /// Represents a content type where the content is a video.
        /// </summary>
        VIDEO,

        /// <summary>
        /// Represents a content type where the content is audio.
        /// </summary>
        AUDIO
    }
}