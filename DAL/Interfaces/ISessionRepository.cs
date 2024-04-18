using BL.Domain;
using BL.Domain.Answers;

namespace DAL.Interfaces;

public interface ISessionRepository : IRepository
{
    Session GetSessionById(int id);
    void AddAnswerToSession(int sessionId, Answer answer, FlowType flowType);
    void Update(Session session);
    IEnumerable<Answer> GetAnswersBySessionId(int sessionId);
}