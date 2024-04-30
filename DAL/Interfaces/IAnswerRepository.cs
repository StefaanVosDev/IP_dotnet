using BL.Domain.Answers;

namespace DAL.Interfaces;

public interface IAnswerRepository : IRepository
{
    public IEnumerable<Answer> GetAnswersByProjectId(int projectId);
}