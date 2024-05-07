using BL.Domain.Questions;

namespace BL.Interfaces;

public interface IQuestionManager : IManager<Question>
{
    public Question GetQuestionById(int questionId);
    Question GetQuestionByIdAndType(int id);
    public IEnumerable<Question> GetQuestionsByFlowId(int flowId);
    public IEnumerable<Question> GetQuestionsBetweenPositions(int newPosition, int oldPosition);
    
    public List<string> GetOptionsSingleOrMultipleChoiceQuestion(int id);
    public (int, int) GetRangeQuestionValues(int id);
    
    public void AddOptionToQuestion(int id, string option);
    public void SetRangeQuestionValues(int id, int min, int max);
    public void DeleteOptionFromQuestion(int id, string option);
}