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

    public void AddAnswerToSession(int sessionId, Answer answer, FlowType flowType)
    {
        var session = context.Sessions.Include(s => s.Answers).FirstOrDefault(s => s.Id == sessionId);
        if (session == null)
        {
            throw new Exception("No session found with this id");
        }

        if (flowType == FlowType.LINEAR)
        {
            //TODO change needed for multiplayer
            //efficient way to see if there already is an answer coupled to a question with this sessionId
            if (session.Answers.Any(a => a.QuestionId == answer.QuestionId))
            {
                session.Answers.Remove(session.Answers.First(a => a.QuestionId == answer.QuestionId));
            }
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

    public IEnumerable<Answer> GetAnswersBySessionId(int sessionId)
    {
        return context.Sessions.Include(s => s.Answers).FirstOrDefault(s => s.Id == sessionId)?.Answers;
    }

    public Answer UpdateAnswer(Answer answerToUpdate, Answer answer)
    {
        var session = context.Sessions.Include(s => s.Answers).FirstOrDefault(s => s.Id == answerToUpdate.Session.Id);
        if (session == null)
        {
            throw new Exception("No session found with this id");
        }
        
        if (session.Answers.Any(a => a.QuestionId == answer.QuestionId))
        {
          context.Answers.Remove(session.Answers.First(a => a.QuestionId == answer.QuestionId));
        }
        session.Answers.Add(answer);
        context.SaveChanges();
        return answer;
    }

    private bool SessionExists(int id)
    {
        return context.Sessions.Any(e => e.Id == id);
    }

    public Answer GetAnswerByQuestionId(int sessionId, int questionId)
    {
        return context.Answers.FirstOrDefault(a => a.Session.Id == sessionId && a.QuestionId == questionId);
    }
}