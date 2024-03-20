using BL.Domain.Questions;

namespace BL.Domain.Answers;

public class SingleChoiceAnswer : Answer
{
    public string SelectedOption { get; set; }
    
    public SingleChoiceAnswer()
    {
    }
    
    public SingleChoiceAnswer(Question question, string selectedOption) : base(question)
    {
        SelectedOption = selectedOption;
    }
}