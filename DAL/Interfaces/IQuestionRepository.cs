using BL.Domain.Questions;

namespace DAL.Interfaces;

public interface IQuestionRepository : IRepository
{
    public Question GetQuestionByIdWithMedia(int questionId);
    IEnumerable<Question> GetQuestionsByFlowId(int flowId);
    IEnumerable<Question> GetQuestionsBetweenPositions(int newPosition, int oldPosition);

    public List<string> GetOptionsSingleOrMultipleChoiceQuestion(int id);
    public (int, int) GetRangeQuestionValues(int id);
    
    public void AddOptionToQuestion(int id, string option);
    public void SetRangeQuestionValues(int id, int min, int max);
    public void DeleteOptionFromQuestion(int id, string option);


}