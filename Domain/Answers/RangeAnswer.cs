using BL.Domain.Questions;

namespace BL.Domain.Answers;

public class ScaleAnswer : Answer
{
    public int SelectedValue { get; set; }
    
    public ScaleAnswer()
    {
    }
    
    public ScaleAnswer(Question question, int selectedValue) : base(question)
    {
        SelectedValue = selectedValue;
    }
}