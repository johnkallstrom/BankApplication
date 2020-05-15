﻿using Bank.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Web.Services
{
    public interface IUserService
    {
        Task<bool> DeleteUser(ApplicationUser user);
        Task<bool> EditUser(ApplicationUser user, string password, string role);
        Task<bool> CreateUser(ApplicationUser user, string password, string role);
        Task<string> GetUserRole(ApplicationUser user);
        Task<ApplicationUser> Get(string id);
        IEnumerable<ApplicationUser> GetAll();
        Task<ApplicationUser> GetByEmail(string email);
        Task<SignInResult> SignInUser(string username, string password);
        Task SignOutUser();
        bool IsUserLoggedIn();
    }
}
