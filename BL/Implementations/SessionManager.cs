using BL.Domain;
using BL.Interfaces;
using DAL.Interfaces;
using BL.Domain.Answers;
using DAL;
using DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace BL.Implementations
{
    public class SessionManager(ISessionRepository repository) : Manager<Session>(repository), ISessionManager
    {
        public Session GetSessionById(int id)
        {
            return repository.GetSessionById(id);
        }

        public void UpdateSession(Session session)
        {
            repository.Update(session);
        }

        public void AddAnswerToSession(int sessionId, Answer answer)
        {
            repository.AddAnswerToSession(sessionId, answer);
        }
    }
}