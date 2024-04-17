﻿using A1_COMP2139.Models;
using Microsoft.AspNetCore.Identity;

namespace A1_COMP2139.Data
{
    public class ContextSeed
    {

        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Basic.ToString()));

        }

        public static async Task SuperSeedRoleAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var superUser = new ApplicationUser
            {
                UserName = "superAdmin",
                Email = "adminsupport@domain.com",
                FirstName = "Sherry",
                LastName = "Zarei",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != superUser.Id))
            {
                var user = await userManager.FindByEmailAsync(superUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(superUser, "Admin228!");

                    await userManager.AddToRoleAsync(superUser, Enum.Roles.SuperAdmin.ToString());
                    await userManager.AddToRoleAsync(superUser, Enum.Roles.Basic.ToString());
                }
            }
        }
    }
}
