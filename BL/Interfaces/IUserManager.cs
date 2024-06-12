using Microsoft.AspNetCore.Identity;

namespace BL.Interfaces
{
    public interface IUserManager : IManager<IdentityUser>
    {
        public IdentityUser GetUserById(string id);
        public bool CheckPassword(string user, string password);
    }
}