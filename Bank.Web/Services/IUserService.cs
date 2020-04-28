using Bank.Web.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bank.Web.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetByEmail(string email);
        Task<SignInResult> SignInUser(string username, string password);
        void SignOutUser();
    }
}
