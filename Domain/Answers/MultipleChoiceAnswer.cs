using BL.Domain.Questions;

namespace BL.Domain.Answers;

public class MultipleChoiceAnswer : Answer
{
    public List<string> SelectedOptions { get; set; }
    
    public MultipleChoiceAnswer()
    {
    }
    
    public MultipleChoiceAnswer(Question question, List<string> selectedOptions) : base(question)
    {
        SelectedOptions = selectedOptions;
    }
}