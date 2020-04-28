using Bank.Infrastructure;
using Bank.Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bank.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var iHost = CreateHostBuilder(args).Build();
            InitializeDatabase(iHost);
            iHost.Run();
        }

        private static void InitializeDatabase(IHost iHost)
        {
            using (var scope = iHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                var db = new DatabaseInitializer();
                db.Seed(userManager, roleManager);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
