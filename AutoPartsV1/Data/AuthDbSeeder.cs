using AutoPartsV1.Auth.Model;
using Microsoft.AspNetCore.Identity;

namespace AutoPartsV1.Data
{
    public class AuthDbSeeder
    {
        private readonly UserManager<AutoPartsV1RestUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthDbSeeder(UserManager<AutoPartsV1RestUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await AddDefaultRoles();
            await AddAdminUser();
        }

        private async Task AddAdminUser()
        {
            var newAdminUser = new AutoPartsV1RestUser()
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if (existingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "SafePassword1!");
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, AutoPartsV1Roles.All);
                }
            }
        }

        private async Task AddDefaultRoles()
        {
            foreach (var role in AutoPartsV1Roles.All)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
