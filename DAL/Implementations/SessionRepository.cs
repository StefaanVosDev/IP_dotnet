using BL.Domain;
using BL.Domain.Answers;
using DAL.EF;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class SessionRepository(PhygitalDbContext context) : Repository(context), ISessionRepository
{
    public Session GetSessionById(int id)
    {
        return context.Set<Session>().Find(id);
    }

    public void AddAnswerToSession(int sessionId, Answer answer)
    {
        var session = context.Sessions.Include(s => s.Answers).FirstOrDefault(s => s.Id == sessionId);
        if (session == null)
        {
            throw new Exception("No session found with this id");
        }
        session.Answers.Add(answer);
        context.SaveChanges();
    }

    public void Update(Session session)
    {
        context.Attach(session).State = EntityState.Modified;
        try
        {
            context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SessionExists(session.Id))
            {
                throw new Exception("No session found with this id");
            }
            else
            {
                throw;
            }
        }
    }

    private bool SessionExists(int id)
    {
        return context.Sessions.Any(e => e.Id == id);
    }
}