using BL.Domain.Questions;

namespace BL.Interfaces;

public interface IQuestionManager : IManager<Question>
{
    public Question GetQuestionById(int questionId);
    Question GetQuestionByIdAndType(int id);
}