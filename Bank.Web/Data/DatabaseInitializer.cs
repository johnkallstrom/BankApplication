using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bank.Web.Data
{
    public class DatabaseInitializer
    {
        public void Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AddRole(roleManager, "Admin").Wait();
            AddRole(roleManager, "Cashier").Wait();
            AddUser(userManager, "admin", "admin@mail.com", "Admin").Wait();
        }

        private async Task AddRole(RoleManager<IdentityRole> roleManager, string role)
        {
            if (await roleManager.RoleExistsAsync(role) == false)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task AddUser(
            UserManager<ApplicationUser> userManager, 
            string username, 
            string email, 
            string role)
        {
            if (await userManager.FindByNameAsync(username) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = username,
                    Email = email,
                };

                const string password = "password";
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
