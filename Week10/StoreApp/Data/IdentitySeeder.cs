using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StoreApp.Data
{
    public static class IdentitySeeder
    {
        private const string AdminEmail = "admin@store.com";
        private const string AdminPassword = "admin123";

        public static async Task SeedIdentity(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager
        )
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Role.Client))
                await roleManager.CreateAsync(new IdentityRole { Name = Role.Client });

            if (!await roleManager.RoleExistsAsync(Role.Admin))
                await roleManager.CreateAsync(new IdentityRole { Name = Role.Admin });
        }

        private static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (await userManager.FindByNameAsync(AdminEmail) is null)
            {
                var user = new IdentityUser { UserName = AdminEmail, Email = AdminEmail };
                await userManager.CreateAsync(user, AdminPassword);
                await userManager.AddToRoleAsync(user, Role.Admin);
            }
        }
    }
}
