using BL.Domain.Questions;

namespace DAL.Interfaces;

public interface IQuestionRepository : IRepository
{
    public Question GetQuestionById(int questionId);
    IEnumerable<Question> GetQuestionsByFlowId(int flowId);
    IEnumerable<Question> GetQuestionsBetweenPositions(int newPosition, int oldPosition);
}