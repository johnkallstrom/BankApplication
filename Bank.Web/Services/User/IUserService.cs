using Bank.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Web.Services
{
    public interface IUserService
    {
        Task<string> GetUserRole(ApplicationUser user);
        Task<ApplicationUser> Get(string id);
        IEnumerable<ApplicationUser> GetAll();
        Task<ApplicationUser> GetByEmail(string email);
        Task<SignInResult> SignInUser(string username, string password);
        Task SignOutUser();
        bool IsUserLoggedIn();
    }
}
