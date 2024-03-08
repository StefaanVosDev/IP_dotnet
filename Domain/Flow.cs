namespace BL.Domain
{
    /// <summary>
    /// Represents a Flow entity with a unique ID, name, theme, description, type, subflows, questions, and contents.
    /// </summary>
    public class Flow
    {
        /// <summary>
        /// Gets or sets the unique identifier for the flow.
        /// </summary>
        public int FlowId { get; set; }

        /// <summary>
        /// Gets or sets the name of the flow.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the theme of the flow.
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Gets or sets the description of the flow.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of the flow.
        /// </summary>
        public FlowType Type { get; set; }

        /// <summary>
        /// Gets or sets the subflows of the flow.
        /// </summary>
        public IEnumerable<Flow> SubFlows { get; set; }

        /// <summary>
        /// Gets or sets the questions of the flow.
        /// </summary>
        public IEnumerable<Question> Questions { get; set; }

        /// <summary>
        /// Gets or sets the contents of the flow.
        /// </summary>
        public IEnumerable<Content> Contents { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Flow"/> class.
        /// </summary>
        public Flow(IEnumerable<Flow> subFlows, IEnumerable<Question> questions, IEnumerable<Content> contents)
        {
            SubFlows = subFlows;
            Questions = questions;
            Contents = contents;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Flow"/> class with the specified name, theme, description, and type.
        /// </summary>
        /// <param name="name">The name of the flow.</param>
        /// <param name="theme">The theme of the flow.</param>
        /// <param name="description">The description of the flow.</param>
        /// <param name="type">The type of the flow.</param>
        /// <param name="subFlows"></param>
        /// <param name="questions"></param>
        /// <param name="contents"></param>
        public Flow(string name, string theme, string description, FlowType type, IEnumerable<Flow> subFlows, IEnumerable<Question> questions, IEnumerable<Content> contents)
        {
            Name = name;
            Theme = theme;
            Description = description;
            Type = type;
            SubFlows = subFlows;
            Questions = questions;
            Contents = contents;
        }
    }
}