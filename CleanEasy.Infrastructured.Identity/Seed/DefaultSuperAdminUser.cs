using CleanEasy.Core.Application.Enum;
using CleanEasy.Infrastructured.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanEasy.Infrastructured.Identity.Seed
{
    public static class DefaultSuperAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser applicationUser = new();
            applicationUser.Name = "Juan Leonardo";
            applicationUser.Email = "addieljaquez@gmail.com";
            applicationUser.PhoneNumber = "829-967-8897";
            applicationUser.PhoneNumberConfirmed = true;
            applicationUser.LastActivity = "Register";
            applicationUser.State = false;
            applicationUser.EmailConfirmed = false;
            if (userManager.Users.All(u => u.Id == applicationUser.Id))
            {
                var user = await userManager.FindByEmailAsync(applicationUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(applicationUser);
                    await userManager.AddToRoleAsync(applicationUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(applicationUser, Roles.SuperAdmin.ToString());
                    await userManager.AddToRoleAsync(applicationUser, Roles.Admin.ToString());
                }
            }

        }
    }
}
