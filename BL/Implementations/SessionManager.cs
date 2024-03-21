using BL.Domain;

namespace BL.Implementations;

public class SessionManager
{
    private readonly Dictionary<string, Session> _sessions;

    public SessionManager()
    {
        _sessions = new Dictionary<string, Session>();
    }

    public void StartSession(string userId, int flowId, int questionId)
    {
        var session = new Session
        {
            UserId = userId,
            FlowId = flowId,
            QuestionId = questionId
        };

        _sessions[userId] = session;
    }

    public Session GetSession(string userId)
    {
        _sessions.TryGetValue(userId, out var session);
        return session;
    }

    public void UpdateSession(string userId, string answer)
    {
        if (_sessions.TryGetValue(userId, out var session))
        {
            session.Answer = answer;
        }
    }

    public void EndSession(string userId)
    {
        _sessions.Remove(userId);
    }
}