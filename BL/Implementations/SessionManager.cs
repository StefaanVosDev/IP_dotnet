using BL.Domain;
using BL.Interfaces;
using DAL.Interfaces;
using BL.Domain.Answers;
using DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace BL.Implementations
{
    public class SessionManager : Manager<Session>, ISessionManager
    {
        private readonly PhygitalDbContext _context;

        public SessionManager(ISessionRepository repository, PhygitalDbContext context) : base(repository)
        {
            _context = context;
        }

        public Session GetSession(string userId)
        {
            return _context.Sessions.Include(s => s.Answers).FirstOrDefault(s => s.UserId == userId);
        }

        public void AddAnswerToSession(Answer answer, string userId)
        {
            var session = GetSession(userId);
            if (session != null)
            {
                session.Answers.Add(answer);
                _context.SaveChanges();
            }
        }

        public void UpdateSession(Session session)
        {
            _context.Sessions.Update(session);
            _context.SaveChanges();
        }
    }
}