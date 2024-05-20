using BL.Domain;
using BL.Domain.Questions;

namespace IP_MVC.Models.Dto;

public class QuestionDto
{
    public int Id { get; set; }
    public int Position { get; set; }
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public Media Media { get; set; }
    public int FlowId { get; set; }
}