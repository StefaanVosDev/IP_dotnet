using DAL.EF;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DAL.Implementations;

public class UserRepository(PhygitalDbContext context) : Repository(context), IUserRepository
{
    public IdentityUser GetUserById(string id)
    {
        return context.Users.Find(id);
    }
}