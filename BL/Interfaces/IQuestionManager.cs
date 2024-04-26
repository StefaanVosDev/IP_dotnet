using BL.Domain.Questions;

namespace BL.Interfaces;

public interface IQuestionManager : IManager<Question>
{
    public Question GetQuestionById(int questionId);
    Question GetQuestionByIdAndType(int id);
    public IEnumerable<Question> GetQuestionsByFlowId(int flowId);
    public IEnumerable<Question> GetQuestionsBetweenPositions(int newPosition, int oldPosition);
}