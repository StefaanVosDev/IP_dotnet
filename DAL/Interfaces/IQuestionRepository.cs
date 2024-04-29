using BL.Domain.Questions;

namespace DAL.Interfaces;

public interface IQuestionRepository : IRepository
{
    public Question GetQuestionByIdWithMedia(int questionId);
    IEnumerable<Question> GetQuestionsByFlowId(int flowId);
    IEnumerable<Question> GetQuestionsBetweenPositions(int newPosition, int oldPosition);
}