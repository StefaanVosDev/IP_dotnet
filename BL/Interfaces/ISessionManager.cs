using BL.Domain;
using BL.Domain.Answers;

namespace BL.Interfaces;

public interface ISessionManager : IManager<Session>
{
    public Session GetSessionById(int id);

    public void UpdateSession(Session session);
    public void AddAnswerToSession(int sessionId, Answer answer);
}