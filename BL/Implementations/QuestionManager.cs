using BL.Domain.Questions;
using BL.Interfaces;
using DAL.Interfaces;

namespace BL.Implementations;

public class QuestionManager(IQuestionRepository repository) : Manager<Question>(repository), IQuestionManager
{
    public Question GetQuestionById(int questionId)
    {
        return repository.GetQuestionById(questionId);
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

    public IEnumerable<Question> GetQuestionsBetweenPositions(int newPosition, int oldPosition)
    {
        return repository.GetQuestionsBetweenPositions(newPosition, oldPosition);
    }
}