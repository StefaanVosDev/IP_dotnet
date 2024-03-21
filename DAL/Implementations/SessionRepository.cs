using DAL.EF;
using DAL.Interfaces;

namespace DAL.Implementations;

public class SessionRepository(PhygitalDbContext context) : Repository(context), ISessionRepository
{
    
}