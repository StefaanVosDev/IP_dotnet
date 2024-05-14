using BL.Domain.Questions;

namespace IP_MVC.Models;

public class QuestionViewModel
{
    public Question Question { get; set; }
    public string QuestionType { get; set; }
}