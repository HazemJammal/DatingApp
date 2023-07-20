using API.Entities;

namespace API.Interfaces
{
    public interface IAccountRepository
    {
        Task<AppUser> GetUserByUsernameAsync(string username);
        void AddUser(AppUser user);
   
    }
}