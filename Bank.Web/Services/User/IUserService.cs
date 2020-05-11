using Bank.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bank.Web.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetByEmail(string email);
        Task<SignInResult> SignInUser(string username, string password);
        Task SignOutUser();
        bool IsUserLoggedIn();
    }
}
