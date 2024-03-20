using BL.Domain.Questions;

namespace BL.Domain.Answers;

public class RangeAnswer : Answer
{
    public int SelectedValue { get; set; }
    
    public RangeAnswer()
    {
    }
    
    public RangeAnswer(Question question, int selectedValue) : base(question)
    {
        SelectedValue = selectedValue;
    }
}