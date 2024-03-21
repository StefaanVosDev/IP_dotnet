using BL.Domain.Questions;

namespace DAL.Interfaces;

public interface IQuestionRepository : IRepository
{
    public Question GetQuestionById(int questionId);
}