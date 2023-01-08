using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Shwartz",
                        Street = "Няма такава",
                        Country = "Bulgaria",
                        City = "Sofia",
                        ZipCode = "1632"
                    }
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user,"admin");
            }
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var role = new IdentityRole
                {
                    Name = "admin"
                };
                await roleManager.CreateAsync(role);

                role = new IdentityRole
                {
                    Name = "customer"
                };
                await roleManager.CreateAsync(role);
                
                role = new IdentityRole
                {
                    Name = "manager"
                };
                await roleManager.CreateAsync(role);
            }
        }
    }
}