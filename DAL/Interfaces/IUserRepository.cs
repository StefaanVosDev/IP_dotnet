using Microsoft.AspNetCore.Identity;

namespace DAL.Interfaces;

public interface IUserRepository : IRepository
{
    public IdentityUser GetUserById(string id);
}