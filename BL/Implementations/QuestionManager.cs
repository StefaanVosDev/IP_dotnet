using BL.Domain;
using BL.Domain.Questions;
using BL.Interfaces;
using DAL.Interfaces;

namespace BL.Implementations;

public class QuestionManager(IQuestionRepository repository) : Manager<Question>(repository), IQuestionManager
{
    public Question GetQuestionById(int questionId)
    {
        return repository.GetQuestionByIdWithMedia(questionId);
    }
    
    public Question GetQuestionByIdAndType(int id)
    {
        var question = GetQuestionById(id);
        return question.Type switch
        {
            QuestionType.Range => question as RangeQuestion,
            QuestionType.MultipleChoice => question as MultipleChoiceQuestion,
            QuestionType.Open => question as OpenQuestion,
            QuestionType.SingleChoice => question as SingleChoiceQuestion,
            _ => throw new Exception("Unknown question type")
        };
    }

    public IEnumerable<Question> GetQuestionsByFlowId(int flowId)
    {
        return repository.GetQuestionsByFlowId(flowId);
    }
    public IEnumerable<Question> GetQuestionsByFlowIdWithMedia(int flowId)
    {
        return repository.GetQuestionsByFlowId(flowId);
    }

    public IEnumerable<Question> GetQuestionsBetweenPositionsByFlowId(int flowId, int newPosition, int oldPosition)
    {
        return repository.GetQuestionsBetweenPositionsByFlowId(flowId, newPosition, oldPosition);
    }
    
    public List<string> GetOptionsSingleOrMultipleChoiceQuestion(int id)
    {
        return repository.GetOptionsSingleOrMultipleChoiceQuestion(id);
    }
    
    public (int, int) GetRangeQuestionValues(int id)
    {
        return repository.GetRangeQuestionValues(id);
    }
    
    public void AddOptionToQuestion(int id, string option)
    {
        repository.AddOptionToQuestion(id, option);
    }

    public void SetRangeQuestionValues(int id, int min, int max)
    {
        repository.SetRangeQuestionValues(id, min, max);
    }

    public void DeleteOptionFromQuestion(int id, string option)
    {
        repository.DeleteOptionFromQuestion(id, option);
    }
    
    public void AddMediaToQuestion(int id, string filePath, string description, MediaType mediaType)
    {
        repository.AddMediaToQuestion(id, filePath, description, mediaType);
    }
}