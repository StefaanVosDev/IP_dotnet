namespace BL.Domain.Questions
{
    public class SingleChoiceQuestion : Question
    {
        public SingleChoiceQuestion()
        {
        }

        public SingleChoiceQuestion(int position, string text, List<string> options, Media media) : base(position, text, QuestionType.SingleChoice, media)
        {
            Options = options;
        }

        public List<string> Options { get; set; }
    }
}