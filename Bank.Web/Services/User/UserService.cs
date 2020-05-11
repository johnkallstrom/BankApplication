using Bank.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bank.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> GetByEmail(string email) => await _userManager.FindByEmailAsync(email);

        public async Task<SignInResult> SignInUser(string username, string password) => await _signInManager.PasswordSignInAsync(username, password, false, false);

        public Task SignOutUser() => _signInManager.SignOutAsync();

        public bool IsUserLoggedIn() => _signInManager.IsSignedIn(_httpContextAccessor.HttpContext.User);
    }
}
