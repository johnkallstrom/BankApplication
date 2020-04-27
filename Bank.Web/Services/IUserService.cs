using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bank.Web.Services
{
    public interface IUserService
    {
        Task<SignInResult> SignInUser(string email, string password);
        void SignOutUser();
    }
}
