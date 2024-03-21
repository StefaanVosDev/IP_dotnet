using BL.Domain;

namespace BL.Interfaces;

public interface ISessionManager
{
    public void StartSession(string userId, int flowId, int questionId);

    public Session GetSession(string userId);

    public void UpdateSession(string userId, string answer);

    public void EndSession(string userId);
}