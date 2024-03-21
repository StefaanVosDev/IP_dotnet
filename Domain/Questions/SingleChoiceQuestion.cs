namespace BL.Domain.Questions
{
    public class SingleChoiceQuestion : Question
    {
        public SingleChoiceQuestion()
        {
        }

        public SingleChoiceQuestion(string text, List<string> options) : base(text, QuestionType.SingleChoice)
        {
            Options = options;
        }

        public List<string> Options { get; set; }
    }
}