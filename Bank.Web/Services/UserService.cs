using Bank.Web.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bank.Web.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> SignInUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            return result;
        }

        public async void SignOutUser()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
