using Bank.Infrastructure.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> DeleteUser(ApplicationUser user);
        Task<bool> EditUser(string id, string email, string password, string currentRole, string newRole);
        Task<bool> CreateUser(ApplicationUser user, string password, string role);
        Task<string> GetUserRole(ApplicationUser user);
        Task<ApplicationUser> Get(string id);
        IEnumerable<ApplicationUser> GetAll();
        Task<ApplicationUser> GetByEmail(string email);
    }
}
