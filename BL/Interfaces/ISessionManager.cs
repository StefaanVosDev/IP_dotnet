using BL.Domain;
using BL.Domain.Answers;
using BL.Domain.Questions;

namespace BL.Interfaces;

public interface ISessionManager : IManager<Session>
{
    public Session GetSessionById(int id);

    public void UpdateSession(Session session);
    public void AddAnswerToSession(int sessionId, Answer answer, bool linearFlow);
    public IEnumerable<Answer> GetAnswersBySessionId(int sessionId);
    Task<Session> CreateNewSession(int flowId);
    IEnumerable<Question> GetQuestionsBySessionId(int sessionId);
    void SaveAnswer(string answerText, int questionId, int sessionId, bool linearFlow = true);
}