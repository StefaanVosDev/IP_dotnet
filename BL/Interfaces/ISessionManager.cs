using BL.Domain;
using BL.Domain.Answers;

namespace BL.Interfaces;

public interface ISessionManager : IManager<Session>
{
    Session GetSession(string userId);
    void AddAnswerToSession(Answer answer, string userId);
    void UpdateSession(Session session);
}