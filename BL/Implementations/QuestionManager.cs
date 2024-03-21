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
}