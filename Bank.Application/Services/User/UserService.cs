using Bank.Application.Exceptions;
using Bank.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> DeleteUser(ApplicationUser user)
        {
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded) return true;
            else return false;
        }

        public async Task<bool> EditUser(string id, string email, string password, string currentRole, string newRole)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;
            if (currentRole == newRole) throw new MatchingRolesException("The new role can't be the same as the existing role.");

            if (user.Email != email)
            {
                var token = await _userManager.GenerateChangeEmailTokenAsync(user, email);
                await _userManager.ChangeEmailAsync(user, email, token);
            }

            if (!string.IsNullOrWhiteSpace(password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, password);
            }

            if (currentRole != newRole && !string.IsNullOrEmpty(newRole))
            {
                await _userManager.RemoveFromRoleAsync(user, currentRole);
                await _userManager.AddToRoleAsync(user, newRole);
            }

            user.UserName = email;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return true;
            else return false;
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
                throw new UserExistsException("The email address entered already exists.");
            }
        }

        public async Task<string> GetUserRole(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }

        public async Task<ApplicationUser> Get(string id) => await _userManager.FindByIdAsync(id);

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _userManager.Users
                .OrderBy(u => u.Email)
                .ToList();
        }

        public async Task<ApplicationUser> GetByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) throw new EmailNotFoundException("The email you entered does not exist.");

            return user;
        }
    }
}
