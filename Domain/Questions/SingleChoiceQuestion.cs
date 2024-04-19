namespace BL.Domain.Questions
{
    public class SingleChoiceQuestion : Question
    {
        public SingleChoiceQuestion()
        {
        }

        public SingleChoiceQuestion(string text, List<string> options, Media media) : base(text, QuestionType.SingleChoice, media)
        {
            Options = options;
        }

        public List<string> Options { get; set; }
    }
}