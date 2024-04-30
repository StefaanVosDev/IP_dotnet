using BL.Domain.Answers;
using BL.Interfaces;
using DAL.Interfaces;

namespace BL.Implementations;

public class AnswerManager(IAnswerRepository repository) : Manager<Answer>(repository), IAnswerManager
{
    public IEnumerable<Answer> GetAnswersByProjectId(int projectId)
    {
        return repository.GetAnswersByProjectId(projectId);
    }
}