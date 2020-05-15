using Bank.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> DeleteUser(ApplicationUser user)
        {
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded) return true;
            else return false;
        }

        public async Task<bool> EditUser(ApplicationUser user, string password, string role)
        {
            // TODO: Edit User
            return true;
        }

        public async Task<bool> CreateUser(ApplicationUser user, string password, string role)
        {
            if (await _userManager.FindByEmailAsync(user.Email) == null)
            {

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded) await _userManager.AddToRoleAsync(user, role);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> GetUserRole(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }

        public async Task<ApplicationUser> Get(string id) => await _userManager.FindByIdAsync(id);

        public IEnumerable<ApplicationUser> GetAll() => _userManager.Users.ToList();

        public async Task<ApplicationUser> GetByEmail(string email) => await _userManager.FindByEmailAsync(email);

        public async Task<SignInResult> SignInUser(string username, string password) => await _signInManager.PasswordSignInAsync(username, password, false, false);

        public Task SignOutUser() => _signInManager.SignOutAsync();

        public bool IsUserLoggedIn() => _signInManager.IsSignedIn(_httpContextAccessor.HttpContext.User);
    }
}
